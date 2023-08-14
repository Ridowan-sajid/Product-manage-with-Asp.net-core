using First.Model;
using Microsoft.EntityFrameworkCore;

//In this file we are configuring database settings which is must
namespace First_project.Data
{
    public class ApplicationDbContext:DbContext
    {
        //ctor=to create contructor shortcutly
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
            ):base(options) {}
       
        //After creating this constructor we have to register it, we will register it to Program.cs
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
               new Category { Id = 2, Name = "Adventure", DisplayOrder = 2 },
                new Category { Id = 4, Name = "Thriller", DisplayOrder = 4 },
               new Category { Id = 3, Name = "Horror", DisplayOrder = 3 }
                      
               );
        }



    }
}
