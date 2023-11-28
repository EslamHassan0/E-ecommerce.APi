using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF.Models.Generic_Repository
{
    public interface IGeneric_repository<T> where T : class
    {
        Task<T> Getbyid(int id);
        Task<T> PostAsync(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<IEnumerable<T>> GetALLAsync();
        T FindProduct(Func<T,bool> condition);
    }
     
}
