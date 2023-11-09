using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Entities.DTOs.UserDTOs
{
    public class UserLoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
