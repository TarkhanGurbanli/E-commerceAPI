using Bogus;
using EcommerceApi.DataAccess.Concrete.EntityFramework;
using EcommerceApi.Entities.Concrete;

//Ilk once Bogus yuklemek lazimdir, sonra burada random Datalar gotururuk

namespace EcommerceApi.DataAccess.DataHelper
{
    public static class DataSeeder
    {
        public static void AddData()
        {
            using AppDbContext context = new();

            if (!context.Categories.Any())
            {
                var fakeCategories = new Faker<Category>();

                fakeCategories.RuleFor(x => x.CategoryName, z => z.Commerce.Categories(1)[0]);
                fakeCategories.RuleFor(x => x.PhotoUrl, z => z.Image.PicsumUrl());
                fakeCategories.RuleFor(x => x.Status, z => z.Random.Bool());
                fakeCategories.RuleFor(x => x.CreatedDate, z => z.Date.Recent());

                var categories = fakeCategories.Generate(80);
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

        }
        public static void AddDataProduct()
        {
            using AppDbContext context = new();
            if (!context.Products.Any())
            {
                var fakeProducts = new Faker<Product>();

                fakeProducts.RuleFor(x => x.ProductName, z => z.Commerce.Categories(1)[0]);
                fakeProducts.RuleFor(x => x.PhotoUrl, z => z.Image.PicsumUrl());
                fakeProducts.RuleFor(x => x.Status, z => z.Random.Bool());
                fakeProducts.RuleFor(x => x.CreatedDate, z => z.Date.Recent());
                fakeProducts.RuleFor(x => x.Price, z => z.Random.Decimal(10, 1000));
                fakeProducts.RuleFor(x => x.IsFeatured, z => z.Random.Bool());
                fakeProducts.RuleFor(x => x.Quantity, z => z.Random.Number(1, 10));
                fakeProducts.RuleFor(x => x.Description, z => z.Random.String(10, 30));

                var products = fakeProducts.Generate(30);
                context.Products.AddRange(products);
                context.SaveChanges();
            }

        }

    }
}
