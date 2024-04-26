using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public Category Category { get; set; }
        public EditModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void OnGet(int? id)
        {   
            if (id != null & id != 0) {
                Category = _appDbContext.Categories.Find(id);
            }
            

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
                string name = Category.CategoryName;
                _appDbContext.Update(Category);
                _appDbContext.SaveChanges();
                TempData[("success")] = name + " updated successfully";
                return RedirectToPage("Index");
            }
            return Page();          

        }   
            
    }
}
