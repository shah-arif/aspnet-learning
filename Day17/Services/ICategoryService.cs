public interface ICategoryService
{
    Task<List<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto?> GetByIdAsync(int id);
    Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto);
    Task<List<ProductResponseDto>> GetAllByCategoryAsync(int categoryId);
}