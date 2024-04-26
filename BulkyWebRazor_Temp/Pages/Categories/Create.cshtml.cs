using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        //[BindProperty]
        public Category Category { get; set; }        

        public CreateModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void OnGet() { }
        public IActionResult OnPost()
        {           
            _appDbContext.Add(Category);
            _appDbContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
