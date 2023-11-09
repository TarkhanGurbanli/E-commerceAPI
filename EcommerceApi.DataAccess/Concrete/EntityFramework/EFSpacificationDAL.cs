using EcommerceApi.Core.DataAccess.EntityFramework;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;

namespace EcommerceApi.DataAccess.Concrete.EntityFramework
{
    public class EFSpacificationDAL : EFRepositoryBase<Spacification, AppDbContext>, ISpacificationDAL
    {
        public void AddSpecification(int productId, List<Spacification> specifications)
        {
            using var context = new AppDbContext();

            var result = specifications
                .Select(x => { x.ProductId = productId; x.CreatedDate = DateTime.Now; return x; })
                .ToList();

            context.Spacifications.AddRange(result);
            context.SaveChanges();
        }
    }
}
