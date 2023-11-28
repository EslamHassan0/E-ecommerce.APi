using Assignment.Models;
using AutoMapper;
using E_ecommerceAssignment.APi.Filters;
using E_ecommerceAssignment.EF;
using E_ecommerceAssignment.EF.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;

namespace E_ecommerceAssignment.APi.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
       // private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           // _mapper = mapper;
        }
        //[Authorize(Roles = "Admin,User")]
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct()
            =>Ok(await _unitOfWork.Product.GetALLAsync());

        [HttpGet("GetProductById/{Id}")]
        public async Task<IActionResult> GetProduct(int Id)
        {
           return Ok(await _unitOfWork.Product.Getbyid(Id));

        }
        [HttpPost("CreateProudct")]
        [ServiceFilter(typeof(CheckProductEsistesFilter))]
        public async Task<IActionResult> CreateProudct(Product product)
        {

            var post = await _unitOfWork.Product.PostAsync(product);
            _unitOfWork.Complete();
            return Ok(post);
        }
        [HttpPut("UpdateProudct")]
        public async Task<IActionResult> UpdateProudct(Product product)
         => Ok(_unitOfWork.Product.Update(product));
        [HttpDelete("DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
            => Ok(await _unitOfWork.Product.Delete(Id));


        //APi Controlal
        [HttpPost, DisableRequestSizeLimit]
        [Route("Upload")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("ProcductImage");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { fileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
