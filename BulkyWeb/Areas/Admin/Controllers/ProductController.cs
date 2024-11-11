using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.InterfaceRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;



namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //inject class AppDbContext
        //private readonly AppDbContext _appDbContext;
        //private readonly InterfaceProductRepository  _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            // _productRepo = appDbContext; 
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    ValueBuffer = u.Id.ToString

                });

            return View(objProductList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            //if (newProduct.ProductName == newProduct.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("DisplayOrder",
            //        "the DisplayOrder cannot exactly match the CategroyName");
            //}
            //if (newProduct.ProductName.ToLower() == "7")
            //{
            //    ModelState.AddModelError("",
            //        "7 is an invalid value");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(newProduct);
                _unitOfWork.Save();
                TempData["success"] = "New Product created successfully";

                // return RedirectToAction("Index", "Product");
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? productFromDb2 = _appDbContext.Categories.FirstOrDefault(obj=>obj.Id==id);
            // Product? productFromDb3 = _appDbContext.Categories.Where(obj=>obj.Id == id).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product newProduct)
        {

            if (ModelState.IsValid)
            {
                string name = newProduct.Title;
                _unitOfWork.Product.Update(newProduct);
                _unitOfWork.Save();
                TempData["success"] = name + " updated successfully";
                return RedirectToAction("Index", "Product");


            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Delete(Product newProduct)
        {
            string name = newProduct.Title;
            _unitOfWork.Product.Remove(newProduct);
            _unitOfWork.Save();
            TempData["success"] = name + " deleted successfully";
            return RedirectToAction("Index", "Product");

        }

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int id)
        //{
        //    Product? productFromDb = _appDbContext.Categories.Find(id);
        //    _appDbContext.Remove(productFromDb);
        //    _appDbContext.SaveChanges();
        //    TempData[("success")] = productFromDb.ProductName + "deleted successfully";
        //    return RedirectToAction("Index", "Product");

        //}
    }
}
