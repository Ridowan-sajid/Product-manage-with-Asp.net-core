using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace First.DAL.Repository.IRepository
{
    //We are assuming we will use this for many classes that's why we used T as generic class
    //Example: T= Category
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        //Category category2 = _db.Categories.FirstOrDefault(c => c.Id == id);
        //When we have to pass this (c => c.Id == id) to a function, we will use Expression<>
        //bool=return type
        T Get(Expression<Func<T,bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveMultiple(IEnumerable<T> entity);

        //Update is more complex that's why will write update in another file.
    }
}
