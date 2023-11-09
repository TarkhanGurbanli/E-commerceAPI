using EcommerceApi.Core.Entities.Concrete;
using EcommerceApi.Entities.Concrete;

namespace EcommerceApi.Entities.DTOs.RoleDTOs
{
    public class RemoveUserToRoleDTO
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
