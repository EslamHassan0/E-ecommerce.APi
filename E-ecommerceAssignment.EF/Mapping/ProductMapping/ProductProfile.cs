using Assignment.Models;
using AutoMapper;
using E_ecommerceAssignment.EF.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF.Mapping.ProductMapping
{
    public class ProductProfile  : Profile
    {
        public ProductProfile()
        {
                CreateMap<ProductDTos, Product>();
        }
    }
}
