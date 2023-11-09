using EcommerceApi.Core.Entities.Abstract;

namespace EcommerceApi.Entities.Concrete
{
    public class WishList : BaseEntity, IEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
