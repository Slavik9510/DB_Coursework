using DB_Coursework_API.Helpers;
using DB_Coursework_API.Models.DTO;

namespace DB_Coursework_API.Interfaces
{
    public interface IInventoryRepository
    {
        Task<PagedList<InventoryItemToOrder>> GetItemsToOrderAsync(PaginationParams paginationParams);
    }
}
