public interface IOrderService
{
    Task<OrderResponseDto> CreateAsync(OrderCreateDto dto);
    Task<OrderResponseDto> GetByIdAsync(int id);
    Task<List<OrderResponseDto>> GetAllAsync();
}