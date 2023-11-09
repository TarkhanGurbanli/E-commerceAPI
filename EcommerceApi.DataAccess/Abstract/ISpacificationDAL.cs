using EcommerceApi.Core.DataAccess;
using EcommerceApi.Entities.Concrete;

namespace EcommerceApi.DataAccess.Abstract
{
    public interface ISpacificationDAL : IRepositoryBase<Spacification>
    {
        void AddSpecification(int productId, List<Spacification> specifications);
    }
}
