using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.InterfaceRepository
{
    public interface InterfaceCategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        //void Save();
    }
}
