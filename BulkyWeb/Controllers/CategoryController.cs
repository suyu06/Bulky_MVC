using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.InterfaceRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;



namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        //inject class AppDbContext
        //private readonly AppDbContext _appDbContext;
        private readonly InterfaceCategoryRepository  _categoryRepo;
        public CategoryController(InterfaceCategoryRepository appDbContext)
        {
            _categoryRepo = appDbContext; 
        }
        
    
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(newCategory);
                _categoryRepo.Save();
                TempData[("success")] = "New Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id==null|| id == 0) {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);
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
                    string name = newCategory.CategoryName;
                _categoryRepo.Update(newCategory);
                _categoryRepo.Save();
                    TempData[("success")] = name + " updated successfully";
                    return RedirectToAction("Index", "Category");
             
               
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
           
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Delete(Category newCategory)
        {
            string name = newCategory.CategoryName;
            _categoryRepo.Remove(newCategory);
            _categoryRepo.Save();
            TempData[("success")] = name + " deleted successfully";
            return RedirectToAction("Index", "Category");

        }

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int id)
        //{
        //    Category? categoryFromDb = _appDbContext.Categories.Find(id);
        //    _appDbContext.Remove(categoryFromDb);
        //    _appDbContext.SaveChanges();
        //    TempData[("success")] = categoryFromDb.CategoryName + "deleted successfully";
        //    return RedirectToAction("Index", "Category");

        //}
    }
}
