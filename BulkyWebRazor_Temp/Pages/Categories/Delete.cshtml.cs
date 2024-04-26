using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public Category Category { get; set; }

        public DeleteModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;           
        }

        public void OnGet(int? id)
        {
            if (id != null & id != 0)
            {
                Category = _appDbContext.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            _appDbContext.Remove(Category);
            _appDbContext.SaveChanges();
            return RedirectToPage("Index");
        }

    }
}
