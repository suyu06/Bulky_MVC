using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.InterfaceRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository :Repository<Category>, InterfaceCategoryRepository
    {

        private AppDbContext _db;
        public CategoryRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
