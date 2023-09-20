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

            ///Including Category which was not showing in Product info
            _db.Products.Include(u => u.Category);
            //_db.Categories == dbSet
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
            
        }

        //we have to pass string? includeProperties=null for including Category
        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter,string? includeProperties=null)
        {
            IQueryable<T> query
                =dbSet.Where(filter);

            
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            

            return query.FirstOrDefault();
           //Category category3 = _db.Categories.Where(c => c.Id == id).FirstOrDefault()

        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query
                = dbSet;

            //In Product info we were not able to see category (which is a relationship key). 
            //So we include it here So that we can see category too with its id
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            //////////
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
