using System.ComponentModel.DataAnnotations;

public class ProductCreateDto
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(100, ErrorMessage = "Product name must be maximum 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(1000, ErrorMessage = "Description must be maximum 1000 characters")]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.01")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public int? CategoryId { get; set; }
}