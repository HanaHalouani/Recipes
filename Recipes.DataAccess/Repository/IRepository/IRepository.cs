using System.Linq.Expressions;

namespace Recipes.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Delete(T entity);
        void DeleteAll(IEnumerable<T> entity);



    }
}
