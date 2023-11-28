using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF.Models.Generic_Repository
{
    public class Generic_repository<T> : IGeneric_repository<T> where T : class
    {
        private readonly E_ecommerceContext _context;

        public Generic_repository(E_ecommerceContext context)
        {
            _context = context;
        }

        public async Task<T> Delete(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            _context.Remove(result);
            return result;
        }

        public T FindProduct(Func<T, bool> condition)
        {
            return _context.Set<T>().Where(condition).FirstOrDefault();
        }

       
        public async Task<IEnumerable<T>> GetALLAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Getbyid(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> PostAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        

        public async Task<T> Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
 
    }
}
