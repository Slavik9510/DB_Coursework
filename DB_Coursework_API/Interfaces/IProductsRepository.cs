using DB_Coursework_API.Helpers;
using DB_Coursework_API.Models.Domain;
using DB_Coursework_API.Models.DTO;

namespace DB_Coursework_API.Interfaces
{
    public interface IProductsRepository
    {
        Task<PagedList<Product>> GetProductsAsync(ProductParams productParams);
        Task<List<ProductDto>> GetAdditionalData(IEnumerable<Product> products);
    }
}
