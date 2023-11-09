using EcommerceApi.Core.DataAccess.EntityFramework;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public List<Category> GetFeaturedCategories()
        {
            using var context = new AppDbContext();
            var categories = context.Categories
                .Include(x => x.Products)
                .Where(x => x.Status == true)
                .Take(12).ToList();

            return categories;
        }

        //Statusu true olan 10 dene category getir
        public List<Category> GetNavbarCategories()
        {
            using var context = new AppDbContext();

            var categories = context.Categories.Where(x => x.Status == true).Take(10).ToList();
            return categories;
        }

        // Helper method to save the photo and get the URL
        public string SavePhoto(byte[] photoData, string photoFileName)
        {
            // Your code to save the photo (e.g., to a storage system) and return the URL
            // Example: Save to a file and return the file path
            var filePath = $"/photos/{Guid.NewGuid()}-{photoFileName}";
            File.WriteAllBytes(filePath, photoData);
            return filePath;
        }
    }
}
