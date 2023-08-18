using First.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First.DAL.Repository.IRepository
{
    public interface IProductRepo :IRepository<Product>
    {
        void Update(Product product);
        void Save();
    }
}
