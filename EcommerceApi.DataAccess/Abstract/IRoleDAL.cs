using EcommerceApi.Core.DataAccess;
using EcommerceApi.Core.Entities.Concrete;

namespace EcommerceApi.DataAccess.Abstract
{
    public interface IRoleDAL : IRepositoryBase<Role>
    {
        List<Role> GetRoles();
        List<string> GetUserRoles(int userId);
    }
}
