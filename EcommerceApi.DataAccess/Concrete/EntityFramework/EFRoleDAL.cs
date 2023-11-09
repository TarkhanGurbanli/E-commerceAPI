using EcommerceApi.Core.DataAccess.EntityFramework;
using EcommerceApi.Core.Entities.Concrete;
using EcommerceApi.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.DataAccess.Concrete.EntityFramework
{
    public class EFRoleDAL : EFRepositoryBase<Role, AppDbContext>, IRoleDAL
    {
        private readonly AppDbContext _context;
        public EFRoleDAL(AppDbContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public List<string> GetUserRoles(int userId)
        {
            // Kullanıcının rollerini çekmek için bir sorgu yapılabilir.
            // Örneğin, EF Core kullanılıyorsa:
            var userRoles = _context.AppUsersRoles
                .Where(ur => ur.AppUserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role.RoleName)
                .ToList();

            return userRoles;
        }
    }
}
