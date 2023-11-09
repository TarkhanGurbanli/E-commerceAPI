using AutoMapper;
using EcommerceApi.Business.Abstract;
using EcommerceApi.Business.AutoMapper;
using EcommerceApi.Business.Concrete;
using EcommerceApi.Core.Utilities.MailHelper;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceApi.Business.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static void Run(this IServiceCollection service)
        {
            service.AddScoped<AppDbContext>();

            service.AddScoped<ICategoryService, CategoryManager>();
            service.AddScoped<ICategoryDAL, EFCategoryDAL>();

            service.AddScoped<IProductService, ProductManager>();
            service.AddScoped<IProductDAL, EFProductDAL>();


            service.AddScoped<IOrderService, OrderManager>();
            service.AddScoped<IOrderDAL, EFOrderDAL>();

            service.AddScoped<ISpacificationService, SpacificationManager>();
            service.AddScoped<ISpacificationDAL, EFSpacificationDAL>();

            service.AddScoped<IUserService, UserManager>();
            service.AddScoped<IUserDAL, EFUserDAL>();

            service.AddScoped<IRoleService, RoleManager>();
            service.AddScoped<IRoleDAL, EFRoleDAL>();

            service.AddScoped<IWishListService, WishListManager>();
            service.AddScoped<IWishListDAL, EFWishListDAL>();

            service.AddScoped<IEmailHelper, EmailHelper>();



            //AutoMapper i program.cs de cagirmaq ayaga qaldrimaq ucun 
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}
