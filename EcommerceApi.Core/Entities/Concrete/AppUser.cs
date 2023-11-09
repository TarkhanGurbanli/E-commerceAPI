using EcommerceApi.Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Core.Entities.Concrete
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiresDate { get; set; }
        public bool EmailConfirmed { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int LoginAttempt { get; set; }
        public DateTime LoginAttemptExpires { get; set; }
    }
}
