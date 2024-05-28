using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.InterfaceRepository
{
   public interface IUnitOfWork
    {
        InterfaceCategoryRepository Category { get; }
        InterfaceProductRepository Product { get; }
        void Save();

        
        
    }
}
