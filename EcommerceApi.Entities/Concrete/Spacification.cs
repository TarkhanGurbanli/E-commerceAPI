using EcommerceApi.Core.Entities.Abstract;

namespace EcommerceApi.Entities.Concrete
{
    //Mehsulun Melumatlari, her key in oz value su var
    public class Spacification : BaseEntity, IEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
