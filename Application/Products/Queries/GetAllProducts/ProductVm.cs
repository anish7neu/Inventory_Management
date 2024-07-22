using Domain.Enums;

namespace InventoryManagementSystem.Application.Products.Queries.GetAllProducts
{
    public class ProductVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductTypes ProductTypes { get; set; }
    }
}