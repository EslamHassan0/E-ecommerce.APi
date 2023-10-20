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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly E_ecommerceContext _context;
        public IGeneric_repository<Product> Product { get; private set; }

        public UnitOfWork(E_ecommerceContext context)
        {
            _context = context;
            Product = new Generic_repository<Product>(_context);    
        }

       

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
