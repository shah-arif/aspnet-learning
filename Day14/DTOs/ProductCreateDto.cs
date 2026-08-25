using System.ComponentModel.DataAnnotations;

public class ProductCreateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Required]
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

}