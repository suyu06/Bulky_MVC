using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Dependency injection: Injecting AppDbContext
        private readonly AppDbContext _db;
        //DbSet for the entity type T
        internal DbSet<T> dbSet;
        //Constructor taking AppDbContext as a dependency
        public Repository(AppDbContext db)
        {
            // Assigning the injected AppDbContext instance to the private field _db
            _db = db;
            // Initializing dbSet with DbSet<T> from the provided AppDbContext,_db.categories==dbSet
            this.dbSet=_db.Set<T>();
        }
        
    
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(T entity)
        {
            dbSet.RemoveRange(entity); 
        }
    }
}
