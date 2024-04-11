using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        //inject class AppDbContext
        private readonly AppDbContext _appDbContext;
        public CategoryController(AppDbContext appDbContext) {
            _appDbContext = appDbContext; 
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _appDbContext.Categories.ToList();
            return View(objCategoryList);
        }
    }
}
