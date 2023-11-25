using AutoMapper;
using E_ecommerceAssignment.EF.Mapping.ProductMapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF
{
    public static class ModuleServiceDependancies
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(ma =>
            {
                ma.AddProfile(new ProductProfile());
            });
            IMapper mapper  = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
