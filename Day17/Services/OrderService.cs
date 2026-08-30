using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponseDto> CreateAsync(OrderCreateDto dto)
    {
        var order = new Order();

        foreach (var item in dto.Items)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (product.Stock < item.Quantity)
            {
                throw new Exception("Not enough stock");
            }

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            };

            order.Items.Add(orderItem);

            product.Stock -= item.Quantity;

            order.Total += item.Quantity * product.Price;
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return new OrderResponseDto
        {
            Id = order.Id,
            Date = order.Date,
            Total = order.Total,
            Items = order.Items.Select(oi => new OrderItemResponseDto
            {
                Id = oi.Id,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name
            }).ToList()
        };
    }

    public async Task<OrderResponseDto> GetByIdAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id)
            ?? throw new Exception("Order not found");

        

        return new OrderResponseDto
        {
            Id = order.Id,
            Date = order.Date,
            Total = order.Total,
            Items = order.Items.Select(oi => new OrderItemResponseDto
            {
                Id = oi.Id,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name
            }).ToList()
        };
    }

    public async Task<List<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _context.Orders
            .Select(o => new OrderResponseDto
            {
                Id = o.Id,
                Date = o.Date,
                Total = o.Total,
                Items = o.Items.Select(oi => new OrderItemResponseDto
                {
                    Id = oi.Id,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name
                }).ToList()
            })
            .ToListAsync();
        return orders;
    }
}