namespace EcommerceApi.Entities.DTOs.OrderDTOs
{
    public class OrderCreateDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
