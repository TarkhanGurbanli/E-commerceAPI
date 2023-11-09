using AutoMapper;
using EcommerceApi.Business.Abstract;
using EcommerceApi.Core.Entities.Concrete;
using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Core.Utilities.Result.Concrete.ErrorResult;
using EcommerceApi.Core.Utilities.Result.Concrete.SuccessResult;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.DataAccess.Concrete.EntityFramework;
using EcommerceApi.Entities.DTOs.RoleDTOs;

namespace EcommerceApi.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleDAL _roleDAL;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public RoleManager(IRoleDAL roleDAL, IMapper mapper, AppDbContext context)
        {
            _roleDAL = roleDAL;
            _mapper = mapper;
            _context = context;
        }

        public IResult AddUserRole(AddUserToRoleDTO addUserToRoleDTO)
        {
            try
            {
                if (addUserToRoleDTO == null)
                    throw new ArgumentNullException(nameof(addUserToRoleDTO), "AddUserToRoleDTO cannot be null");

                var user = _context.Users.FirstOrDefault(x => x.Id == addUserToRoleDTO.UserId);
                var role = _context.Roles.FirstOrDefault(x => x.Id == addUserToRoleDTO.RoleId);

                if (user != null && role != null)
                {
                    var userRole = new AppUserRole { AppUserId = addUserToRoleDTO.UserId, RoleId = addUserToRoleDTO.RoleId };
                    _context.AppUsersRoles.Add(userRole);
                    _context.SaveChanges();

                    return new SuccessResult("User added to role successfully");
                }
                else
                {
                    return new ErrorResult("User or role not found");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while adding the role: {ex.Message}");
            }
        }


        public IResult CreateRole(CreateRoleDTO createRoleDTO)
        {
            try
            {
                if (createRoleDTO == null)
                    throw new ArgumentNullException(nameof(createRoleDTO), "CreateRoleDTO cannot be null");

                if (_roleDAL.Any(x => x.RoleName == createRoleDTO.RoleName))
                    return new ErrorResult("A Role with the same name already exists.");

                var map = _mapper.Map<Role>(createRoleDTO);

                _roleDAL.Add(map);

                return new SuccessResult("Role Added!");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while adding the Role: {ex.Message}");
            }
        }

        public IDataResult<List<AllRoleDTO>> GetAllRoles()
        {
            try
            {
                var roles = _roleDAL.GetRoles();

                var roleDTOs = roles.Select(role => new AllRoleDTO
                {
                    Id = role.Id,
                    Name = role.RoleName,
                }).ToList();

                return new SuccessDataResult<List<AllRoleDTO>>(roleDTOs, "Roles retrieved successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AllRoleDTO>>($"An error occurred while retrieving roles: {ex.Message}");
            }
        }

        public IResult RemoveRole(int roleId)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

                if (role != null)
                {
                    var usersInRole = _context.AppUsersRoles.Where(ur => ur.RoleId == roleId).ToList();
                    _context.AppUsersRoles.RemoveRange(usersInRole);

                    _context.Roles.Remove(role);
                    _context.SaveChanges();

                    return new SuccessResult("Role removed successfully");
                }
                else
                {
                    return new ErrorResult("Role not found");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while removing the role: {ex.Message}");
            }
        }


        public IResult RemoveUserRole(AddUserToRoleDTO addUserToRoleDTO)
        {
            try
            {
                if (addUserToRoleDTO == null)
                    throw new ArgumentNullException(nameof(addUserToRoleDTO), "AddUserToRoleDTO cannot be null");

                var user = _context.Users.FirstOrDefault(x => x.Id == addUserToRoleDTO.UserId);
                var role = _context.Roles.FirstOrDefault(x => x.Id == addUserToRoleDTO.RoleId);

                if (user != null && role != null)
                {
                    var userRole = _context.AppUsersRoles.FirstOrDefault(x => x.AppUserId == addUserToRoleDTO.UserId && x.RoleId == addUserToRoleDTO.RoleId);

                    if (userRole != null)
                    {
                        _context.AppUsersRoles.Remove(userRole);
                        _context.SaveChanges();

                        return new SuccessResult("User removed from role successfully");
                    }
                    else
                    {
                        return new ErrorResult("User is not in the specified role");
                    }
                }
                else
                {
                    return new ErrorResult("User or role not found");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while removing the role: {ex.Message}");
            }
        }

        public IResult UpdateRole(UpdateRoleDTO updateRoleDTO)
        {
            try
            {
                if (updateRoleDTO == null)
                    throw new ArgumentNullException(nameof(updateRoleDTO), "UpdateRoleDTO cannot be null");

                var role = _context.Roles.FirstOrDefault(r => r.Id == updateRoleDTO.RoleId);

                if (role != null)
                {
                    role.RoleName = updateRoleDTO.NewRoleName;

                    _context.SaveChanges();

                    return new SuccessResult("Role updated successfully");
                }
                else
                {
                    return new ErrorResult("Role not found");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while updating the role: {ex.Message}");
            }
        }

    }
}
