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
    public class CategoryRepo : Repository<Category>, ICategoryRepo
    {
        private ApplicationDbContext _db;
        public CategoryRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
