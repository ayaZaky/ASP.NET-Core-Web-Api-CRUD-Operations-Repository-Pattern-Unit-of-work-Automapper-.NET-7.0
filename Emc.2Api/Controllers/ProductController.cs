using AutoMapper;
using Emc2.Core.DtoModels;
using Emc2.Core.Models;
using Emc2.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using Emc2.EF.DBContext;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXml4Net.OPC.Internal;
using Emc2.Api.Helpers;

namespace Emc2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;  
        } 

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult>GetAllAsync()
        {
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync();
                var dtoProducts = _mapper.Map<List<DtoProductDetails>>(products);
                return Ok(dtoProducts);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPagedProducts")]
        public async Task<IActionResult> GetPagedAsync(int page, int pageSize)
        {
            try
            {
                var products = await _unitOfWork.Products.GetPagedAsync(page, pageSize);
                var dtoProducts = _mapper.Map<List<DtoProductDetails>>(products);
                return Ok(dtoProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetProductsById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id);
                if (product == null)
                    return NotFound($"No product was found with ID {id}");
                var dtoProduct = _mapper.Map<DtoProductDetails>(product);
                return Ok(dtoProduct);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddAsync([FromForm] DtoProduct dto)
        {
            try
            {
                ImageOperations.ValidateImage(dto.Image); 
                var product = _mapper.Map<Product>(dto);
                product.Image = await ImageOperations.ConvertImageToByteArray(dto.Image);

                await _unitOfWork.Products.AddAsync(product);
                _unitOfWork.Complete(); 
                 
                return Ok("Product added successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] DtoProduct dto)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id);  
                if (product == null)
                    return NotFound($"No product was found with ID {id}");
                // Update the existing  with new data
                _mapper.Map(dto, product);
                if (dto.Image != null)
                {
                    ImageOperations.ValidateImage(dto.Image);
                    product.Image = await ImageOperations.ConvertImageToByteArray(dto.Image); 
                } 
                _unitOfWork.Products.Update(product);
                _unitOfWork.Complete();
                return Ok("Product updated successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id);
                if (product == null)
                    return NotFound($"No product was found with ID {id}");

                _unitOfWork.Products.Delete(product);
                _unitOfWork.Complete();
                return Ok("Product deleted successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
