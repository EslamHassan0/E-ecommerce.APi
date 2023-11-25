using Assignment.Models;
using E_ecommerceAssignment.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_ecommerceAssignment.APi.Filters
{
    public class CheckProductEsistesFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckProductEsistesFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var product = context.ActionArguments["product"] as Product;
            if(product is not null) 
            {
                var productExists = _unitOfWork.Product.FindProduct(a=> $"{a.Name} {a.ProductCode}" == $"{product.Name} {product.ProductCode}");
                if (productExists != null)
                {
                    context.Result = new BadRequestObjectResult("Product already exists");
                }
            }
        }
    }
}
