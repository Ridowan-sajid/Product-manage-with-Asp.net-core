using First.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Emit;

//In this file we are configuring database settings which is must
namespace First_project.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        //ctor=to create contructor shortcutly
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
            ):base(options) {}
       
        //After creating this constructor we have to register it, we will register it to Program.cs
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        //if we use "OnModelCreating()" function after inheriting "IdentityDbContext", then inside this function we had to use "base.OnModelCreating(modelBuilder)"

        //protected override void OnModelCreating(ModelBuilder modelbuilder)
        //{
        //    base.OnModelCreating(modelBuilder)
        //    modelbuilder.Entity<Category>().HasData(
        //        new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
        //       new Category { Id = 2, Name = "Adventure", DisplayOrder = 2 },
        //        new Category { Id = 4, Name = "Thriller", DisplayOrder = 4 },
        //       new Category { Id = 3, Name = "Horror", DisplayOrder = 3 }

        //       );
        //}


    }
}
