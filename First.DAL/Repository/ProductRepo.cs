using First.DAL.Repository.IRepository;
using First.Model;
using First_project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First.DAL.Repository
{
    public class ProductRepo : Repository<Product>,IProductRepo
    {
        private ApplicationDbContext _db;
        public ProductRepo(ApplicationDbContext db) : base(db)
        {
            _db=db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }


        public void Update(Product product)
        {
            _db.Update(product);
        }
    }
}
