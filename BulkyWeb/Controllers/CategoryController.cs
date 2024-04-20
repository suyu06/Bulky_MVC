using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;


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
        public IActionResult Edit(int? id)
        {
            if (id==null|| id == 0) {
                return NotFound();
            }
            Category? categoryFromDb = _appDbContext.Categories.Find(id);
            //Category? categoryFromDb2 = _appDbContext.Categories.FirstOrDefault(obj=>obj.Id==id);
            // Category? categoryFromDb3 = _appDbContext.Categories.Where(obj=>obj.Id == id).FirstOrDefault();
            if (categoryFromDb==null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category newCategory)
        {           
            if (ModelState.IsValid)
            {
                _appDbContext.Update(newCategory);
                _appDbContext.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

    }
}
