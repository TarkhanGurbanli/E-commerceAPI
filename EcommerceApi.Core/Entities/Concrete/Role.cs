using EcommerceApi.Core.Entities.Abstract;

namespace EcommerceApi.Core.Entities.Concrete
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
