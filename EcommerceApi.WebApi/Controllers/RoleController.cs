using EcommerceApi.Business.Abstract;
using EcommerceApi.Entities.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getRoles")]
        public IActionResult GetRoles()
        {
            var roles = _roleService.GetAllRoles();

            if (roles.Success)
                return Ok(roles);

            return BadRequest();

        }

        [HttpPost("createrole")]
        public IActionResult RoleCreate([FromBody] CreateRoleDTO createRoleDTO)
        {
            var result = _roleService.CreateRole(createRoleDTO);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("addusertorole")]
        public IActionResult AddUserRole([FromBody] AddUserToRoleDTO addUserToRoleDTO)
        {
            if (addUserToRoleDTO == null)
                return BadRequest("Invalid data");

            var result = _roleService.AddUserRole(addUserToRoleDTO);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("removeUserRole")]
        public IActionResult RemoveUserRole([FromBody] AddUserToRoleDTO addUserToRoleDTO)
        {
            if (addUserToRoleDTO == null)
                return BadRequest("Invalid data");


            var result = _roleService.RemoveUserRole(addUserToRoleDTO);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("removeRole/{roleId}")]
        public IActionResult RemoveRole(int roleId)
        {
            var result = _roleService.RemoveRole(roleId);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("updateRole")]
        public IActionResult UpdateRole([FromBody] UpdateRoleDTO updateRoleDTO)
        {
            if (updateRoleDTO == null)
                return BadRequest("Invalid data");

            var result = _roleService.UpdateRole(updateRoleDTO);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
