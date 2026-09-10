public interface IProductService
{
    Task<PagedResult<ProductResponseDto>> GetAllAsync(ProductQueryDto request);
    Task<ProductResponseDto?> GetByIdAsync(int id);
    Task<ProductResponseDto> CreateAsync(ProductCreateDto dto);
    Task<bool> UpdateAsync(int id, ProductUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}