using EcommerceApi.Entities.DTOs.OrderDTOs;

namespace EcommerceApi.Entities.DTOs.UserDTOs
{
    public class UserOrderDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public List<OrderUserDTO> OrderUserDTOs { get; set; }
    }
}
