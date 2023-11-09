using EcommerceApi.Core.Entities.Abstract;
using System.Linq.Expressions;

namespace EcommerceApi.Core.DataAccess
{
    //Yalnis IEntiyden Implement olunan classlar olar biler
    public interface IRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        bool Any(Expression<Func<TEntity, bool>> expression);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);
    }
}
