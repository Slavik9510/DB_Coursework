using DB_Coursework_API.Helpers;
using DB_Coursework_API.Models.Domain;

namespace DB_Coursework_API.Interfaces
{
    public interface IProductsRepository
    {
        Task<PagedList<Product>> GetProductsAsync(ProductParams productParams);
    }
}
