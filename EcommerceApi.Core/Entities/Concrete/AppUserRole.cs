using EcommerceApi.Core.Entities.Abstract;

namespace EcommerceApi.Core.Entities.Concrete
{
    public class AppUserRole : IEntity
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
