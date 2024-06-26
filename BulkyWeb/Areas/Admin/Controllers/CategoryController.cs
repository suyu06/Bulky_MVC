﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.InterfaceRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;



namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //inject class AppDbContext
        //private readonly AppDbContext _appDbContext;
        //private readonly InterfaceCategoryRepository  _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            // _categoryRepo = appDbContext; 
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(newCategory);
                _unitOfWork.Save();
                TempData["success"] = "New Category created successfully";
                return RedirectToAction("Index", "Category");
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromDb2 = _appDbContext.Categories.FirstOrDefault(obj=>obj.Id==id);
            // Category? categoryFromDb3 = _appDbContext.Categories.Where(obj=>obj.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
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
                _unitOfWork.Category.Update(newCategory);
                _unitOfWork.Save();
                TempData["success"] = name + " updated successfully";
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

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
            _unitOfWork.Category.Remove(newCategory);
            _unitOfWork.Save();
            TempData["success"] = name + " deleted successfully";
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
