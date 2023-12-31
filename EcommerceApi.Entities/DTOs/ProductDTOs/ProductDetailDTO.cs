﻿using EcommerceApi.Entities.DTOs.SpecificationDTOs;

namespace EcommerceApi.Entities.DTOs.ProductDTOs
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public string PhotoUrl { get; set; }
        public List<SpecificationListDTO> Specifications { get; set; }
    }
}
