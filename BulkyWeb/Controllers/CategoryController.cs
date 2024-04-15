using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        //inject class AppDbContext
        private readonly AppDbContext _appDbContext;
        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; 
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _appDbContext.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            if (newCategory.CategoryName == newCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder",
                    "the DisplayOrder cannot exactly match the CategroyName");
            }
            //    if (newCategory.CategoryName.ToLower() == "7")
            //    {
            //        ModelState.AddModelError("",
            //            "7 is an invalid value");
            //    }

            if (ModelState.IsValid)
            {
                _appDbContext.Add(newCategory);
                _appDbContext.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
