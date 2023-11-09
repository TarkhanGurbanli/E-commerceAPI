using EcommerceApi.Core.Entities.Concrete;

namespace EcommerceApi.Entities.Concrete
{
    public class User : AppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Order> Orders { get; set; }
    }
}
