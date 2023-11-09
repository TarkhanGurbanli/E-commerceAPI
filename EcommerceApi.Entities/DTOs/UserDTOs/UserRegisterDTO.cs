using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Entities.DTOs.UserDTOs
{
    public class UserRegisterDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
    }
}
