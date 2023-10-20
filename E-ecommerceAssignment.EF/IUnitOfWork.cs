using Assignment.Models;
using E_ecommerceAssignment.EF.Models.Generic_Repository;
using E_ecommerceAssignment.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF
{
    public interface IUnitOfWork: IDisposable
    {
        IGeneric_repository<Product> Product {  get; }
        int Complete();
    }

}
