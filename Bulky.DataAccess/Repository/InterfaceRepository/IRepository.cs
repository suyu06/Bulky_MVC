using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.InterfaceRepository
{
    public interface IRepository<T> where T : class
    {
        // get all categories data
        IEnumerable<T> GetAll();

        // get one piece of data
        T Get(Expression<Func<T,bool>> filter);
        void Add(T entity);

        // because update sometimes has different logic,so we don't use it here
        //void Update(T entity);
        void Remove(T entity);
        void RemoveRange(T entity);
    }
}
