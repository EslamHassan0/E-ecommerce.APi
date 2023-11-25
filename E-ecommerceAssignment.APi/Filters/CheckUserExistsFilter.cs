using Assignment.Models;
using E_ecommerceAssignment.EF;
using E_ecommerceAssignment.EF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_ecommerceAssignment.APi.Filters
{
    public class CheckUserExistsFilter : ActionFilterAttribute
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly AuthrServices _authrServices;
        public CheckUserExistsFilter(IUnitOfWork unitOfWork , AuthrServices authrServices)
        {
                _unitOfWork = unitOfWork;
            _authrServices = authrServices;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.ActionArguments["applicationUser"] as ApplicationUser; 
            if (user is not null)
            {
                var userExists = _authrServices.FindUser(u => $"{u.UserName} {u.PhoneNumber} {u.Email}" == $"{user.UserName} {user.PhoneNumber} {user.Email}");
                if (userExists is not null)
                {
                    context.Result = new BadRequestObjectResult("User already exists");
                }

            }
        }
    }
}
