using First.DAL.Repository.IRepository;
using First_project.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet=_db.Set<T>();
            //_db.Categories == dbSet
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
            
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query
                =dbSet.Where(filter);
            return query.FirstOrDefault();
           //Category category3 = _db.Categories.Where(c => c.Id == id).FirstOrDefault()

        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query
                = dbSet;
            return query.ToList();
            
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);    
        }

        public void RemoveMultiple(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
