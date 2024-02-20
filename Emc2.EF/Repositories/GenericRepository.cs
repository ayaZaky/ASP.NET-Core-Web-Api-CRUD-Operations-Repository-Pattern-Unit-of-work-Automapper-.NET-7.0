using Emc2.Core.IRepositories;
using Emc2.Core.Models;
using Emc2.EF.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.EF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDBContext _dbContext;

        public GenericRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
          
        public async Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize)
        {
            return await _dbContext.Set<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<T> AddAsync(T entity)
        {
              await _dbContext.Set<T>().AddAsync(entity);
              return entity;
        }
        public T Update(T entity)
        {
             
               _dbContext.Set<T>().Update(entity);
            return entity;

        }
        public T Delete(T entity)
        { 
           
              _dbContext.Set<T>().Remove(entity);
               return entity;
                 
        }


        //public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query =  _dbContext.Set<T>(); 
        //    if (includes != null)
        //        foreach (var include in includes)
        //            query = query.Include(include);

        //    return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        //}


    }
}
