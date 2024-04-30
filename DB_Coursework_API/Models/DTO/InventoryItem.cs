namespace DB_Coursework_API.Models.DTO
{
    public class InventoryItemToOrder
    {
        public string ProductName { get; set; }
        public int UnitsInStock { get; set; }
        public int ReorderLevel { get; set; }
        public int UnitsOnOrder { get; set; }
        public string SupplierCompanyName { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPhoneNumber { get; set; }
    }
}