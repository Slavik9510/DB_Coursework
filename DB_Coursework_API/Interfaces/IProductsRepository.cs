using DB_Coursework_API.Helpers;
using DB_Coursework_API.Models.Domain;
using DB_Coursework_API.Models.DTO;

namespace DB_Coursework_API.Interfaces
{
    public interface IProductsRepository
    {
        Task<PagedList<InventoryItem>> GetProductsAsync(ProductParams productParams);
        Task<List<InventoryItem>> GetAdditionalData(IEnumerable<Product> products);
        Task<ProductDetailsDto?> GetDetailsAsync(int id);
    }
}
