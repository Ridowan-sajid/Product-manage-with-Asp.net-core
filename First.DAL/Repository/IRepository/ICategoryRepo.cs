using First.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First.DAL.Repository.IRepository
{
    public interface ICategoryRepo : IRepository<Category>
    {
        void Update(Category category);
        void Save();
    }
}
