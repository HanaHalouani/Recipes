using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Repository.IRepository;
using System.Linq.Expressions;

namespace Recipes.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        private ApplicationDbContext db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();

        }


        void IRepository<T>.Add(T entity)
        {
            dbSet.Add(entity);
        }


        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteAll(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

    }
}
