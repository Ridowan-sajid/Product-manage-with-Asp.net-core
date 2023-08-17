using First.DAL.Repository.IRepository;
using First.Model;
using First_project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace First_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _db;
        public CategoryController(ICategoryRepo db)
        {
            _db = db;
        }

        //Read Category

        public IActionResult Index()
        {
            List<Category> categories = _db.GetAll().ToList();
            return View(categories);
        }

        //Create Category

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            //Custom validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and display order can't be same");
            }

            if (category.Name != null && category.Name.ToLower() == "test")
            {
                //though we didn't mention the name of key , it will show the error in asp-validation-summary 
                ModelState.AddModelError("", "Name can't be test");
            }

            if (ModelState.IsValid)
            {
                _db.Add(category);
                _db.Save();
                TempData["success"] = "Category created successfully";//TempData helps us to notify the user.
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        //Update Category

        public IActionResult UpdateCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category category = _db.Categories.Find(id);
            //Category category2 = _db.Categories.FirstOrDefault(c => c.Id == id);
            Category category2 = _db.Get(c => c.Id == id);

            //Category category3 = _db.Categories.Where(c => c.Id == id).FirstOrDefault()
            if (category2 == null)
            {
                return NotFound();
            }

            return View(category2);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and display order can't be same");
            }

            if (category.Name != null && category.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Name can't be test");
            }

            if (ModelState.IsValid)
            {
                //We don't need to tell asp.net the Id because it will specified id by itself. Though we can tell it by hidden it which is safe side.
                _db.Update(category);
                _db.Save();
                TempData["success"] = "Category updated successfully";//TempData helps us to notify the user.
                return RedirectToAction("Index", "Category");
            }
            return View();

        }


        //Delete Category

        public IActionResult DeleteCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category2 = _db.Get(c => c.Id == id);
            if (category2 == null)
            {
                return NotFound();
            }

            return View(category2);
        }

        //Though both action for delete taking same input we had to change the second action name. Though we change the name we have to tell the real action name which DeleteCategory
        [HttpPost, ActionName("DeleteCategory")]
        public IActionResult DeleteCategory2(int? id)
        {
            Category? category2 = _db.Get(c => c.Id == id); ;

            if (category2 == null)
            {
                return NotFound();
            }
            _db.Remove(category2);
            _db.Save();
            TempData["success"] = "Category Deleted successfully";//TempData helps us to notify the user.
            return RedirectToAction("Index", "Category");

        }


    }


}
