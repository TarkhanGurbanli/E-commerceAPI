using EcommerceApi.Core.Entities.Abstract;

namespace EcommerceApi.Entities.Concrete
{
    public class Category : BaseEntity, IEntity
    {
        public string CategoryName { get; set; }
        public string PhotoUrl { get; set; }
        public List<Product> Products { get; set; }
    }
}
