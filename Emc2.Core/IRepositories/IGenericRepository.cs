using Emc2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Core.IRepositories
{
    public  interface IGenericRepository<T> where T : class
    {


        
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize); 
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        T Update (T enetity);
        T Delete(T enetity);
         
    }
}
