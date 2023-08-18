using First.DAL.Repository.IRepository;
using First.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace First_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepo _db;
        private readonly ICategoryRepo _Cdb;
        public ProductController(IProductRepo db, ICategoryRepo Cdb)
        {
            _db = db;
            _Cdb = Cdb;
        }

        //Read Category

        public IActionResult Index()
        {
            List<Product> products = _db.GetAll().ToList();
            return View(products);
        }

        //Create Product

        public IActionResult CreateProduct()
        {
            //After use @model Product we can't send another model through View 
            //That's why we have to create a List of item then we sent it through
            //ViewBag
            IEnumerable<SelectListItem> CategoryList = _Cdb.GetAll().Select(u=>new SelectListItem
            {
                Text=u.Name, //which we want to show
                Value=u.Id.ToString(), 
                //To use SelectListItem we have to use Text and Value.
            }).ToList();  
            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {


            if (product.Name != null && product.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Product Name can't be test");
            }

            if (ModelState.IsValid)
            {
                _db.Add(product);
                _db.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();

        }

        //Update Product

        public IActionResult UpdateProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _db.Get(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {


            if (product.Name != null && product.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Product Name can't be test");
            }

            if (ModelState.IsValid)
            {
                //We don't need to tell asp.net the Id because it will specified id by itself. Though we can tell it by hidden it which is safe side.
                _db.Update(product);
                _db.Save();
                TempData["success"] = "Product updated successfully";//TempData helps us to notify the user.
                return RedirectToAction("Index", "Product");
            }
            return View();

        }


        //Delete Product

        public IActionResult DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _db.Get(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //Though both action for delete taking same input we had to change the second action name. Though we change the name we have to tell the real action name which DeleteCategory
        [HttpPost, ActionName("DeleteProduct")]
        public IActionResult DeleteProduct2(int? id)
        {
            Product? product = _db.Get(c => c.Id == id); ;

            if (product == null)
            {
                return NotFound();
            }
            _db.Remove(product);
            _db.Save();
            TempData["success"] = "Product Deleted successfully";//TempData helps us to notify the user.
            return RedirectToAction("Index", "Product");

        }
    }
}
