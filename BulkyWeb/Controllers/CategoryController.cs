﻿using BulkyWeb.Data;
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
                TempData[("success")] = "Category created successfully";
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
                string name = newCategory.CategoryName;
                _appDbContext.Update(newCategory);
                _appDbContext.SaveChanges();
                TempData[("success")] = name +" updated successfully";
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
            Category? categoryFromDb = _appDbContext.Categories.Find(id);
           
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
            _appDbContext.Remove(newCategory);
            _appDbContext.SaveChanges();
            TempData[("success")] = name + "deleted successfully";
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
