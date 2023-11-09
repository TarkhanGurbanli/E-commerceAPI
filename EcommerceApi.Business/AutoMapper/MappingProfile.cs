using AutoMapper;
using EcommerceApi.Core.Entities.Concrete;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.DTOs.CategoryDTOs;
using EcommerceApi.Entities.DTOs.OrderDTOs;
using EcommerceApi.Entities.DTOs.ProductDTOs;
using EcommerceApi.Entities.DTOs.RoleDTOs;
using EcommerceApi.Entities.DTOs.SpecificationDTOs;
using EcommerceApi.Entities.DTOs.UserDTOs;
using EcommerceApi.Entities.DTOs.WishListDTOs;

namespace EcommerceApi.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CategoryCreateDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryHomeNavBarDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
            CreateMap<Category, CategoryAdminListDTO>().ReverseMap();
            CreateMap<Category, CategoryFeaturedDTO>()
                .ForMember(x => x.ProductCount, o => o.MapFrom(s => s.Products.Where(p => p.CategoryId == s.Id).Count()));

            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Product, ProductDetailDTO>().ReverseMap();
            CreateMap<Product, ProductFeaturedDTO>().ReverseMap();
            CreateMap<Product, ProductRecentDTO>().ReverseMap();
            CreateMap<Product, ProductFilterDTO>().ReverseMap();


            CreateMap<SpecificationAddDTO, Spacification>().ReverseMap();
            CreateMap<Spacification, SpecificationListDTO>().ReverseMap();

            CreateMap<WishListAddItemDTO, WishList>().ReverseMap();
            CreateMap<WishList, WishListItemDTO>()
                .ForMember(x => x.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(x => x.Price, o => o.MapFrom(s => s.Product.Price));

            CreateMap<OrderCreateDTO, Order>().ReverseMap();
            CreateMap<Order, OrderUserDTO>()
                .ForMember(x => x.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(x => x.Price, o => o.MapFrom(s => s.Product.Price))
                .ForMember(x => x.OrderEnum, o => o.MapFrom(s => Enum.GetName(s.OrderEnum)));

            CreateMap<UserLoginDTO, User>().ReverseMap();
            CreateMap<UserRegisterDTO, User>().ReverseMap();
            CreateMap<User, UserOrderDTO>()
                .ForMember(x => x.OrderUserDTOs, o => o.MapFrom(s => s.Orders));

            CreateMap<Role, CreateRoleDTO>().ReverseMap();
            //CreateMap<AppUserRole,AddUserToRoleDTO>().ReverseMap();
            //CreateMap<Role, AddUserToRoleDTO>().ReverseMap();
            CreateMap<Role, RemoveUserToRoleDTO>().ReverseMap();
            //CreateMap<Role, AllRoleDTO>().ReverseMap();
        }
    }
}
