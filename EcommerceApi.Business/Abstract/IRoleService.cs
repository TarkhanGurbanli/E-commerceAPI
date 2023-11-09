using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Entities.DTOs.RoleDTOs;

namespace EcommerceApi.Business.Abstract
{
    public interface IRoleService
    {
        IResult CreateRole(CreateRoleDTO createRoleDTO);
        IResult AddUserRole(AddUserToRoleDTO addUserToRoleDTO);
        IResult RemoveUserRole(AddUserToRoleDTO removeUserToRoleDTO);
        IResult RemoveRole(int roleId);
        IResult UpdateRole(UpdateRoleDTO updateRoleDTO);
        IDataResult<List<AllRoleDTO>> GetAllRoles();
    }
}
