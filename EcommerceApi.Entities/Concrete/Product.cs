using EcommerceApi.Core.Entities.Abstract;

namespace EcommerceApi.Entities.Concrete
{
    public class Product : BaseEntity, IEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public int Quantity { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Spacification> Spacifications { get; set; }
    }
}
