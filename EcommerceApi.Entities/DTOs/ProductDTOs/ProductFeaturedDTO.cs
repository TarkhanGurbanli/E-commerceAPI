namespace EcommerceApi.Entities.DTOs.ProductDTOs
{
    public class ProductFeaturedDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string PhotoUrl { get; set; }
    }
}
