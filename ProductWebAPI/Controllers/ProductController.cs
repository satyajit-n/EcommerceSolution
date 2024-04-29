using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.Models.Domains;
using ProductWebAPI.Models.DTOs;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _context.PRODUCTS.ToListAsync();

                APIResponseDto response = new()
                {
                    isSuccess = true,
                    displayMessage = "Products data fetched successfully",
                    responseBody = products,
                    supportMessage = null
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                APIResponseDto response = new()
                {
                    isSuccess = false,
                    displayMessage = "Something went wrong!",
                    supportMessage = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _context.PRODUCTS.FirstOrDefaultAsync(x => x.Id == id);
                APIResponseDto response = new();

                if (product == null)
                {
                    response.isSuccess = false;
                    response.displayMessage = $"Product with Id {id}, Not found";
                    return NotFound(response);
                }
                else
                {
                    response.isSuccess = true;
                    response.displayMessage = "Product fetched successfully";
                    response.responseBody = product;
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                APIResponseDto response = new()
                {
                    isSuccess = false,
                    displayMessage = "Something went wrong!",
                    supportMessage = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] PRODUCT product)
        {
            try
            {
                APIResponseDto response = new();
                await _context.PRODUCTS.AddAsync(product);
                await _context.SaveChangesAsync();

                response.isSuccess = true;
                response.displayMessage = "Product added successfully";
                response.responseBody = product;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                APIResponseDto response = new()
                {
                    isSuccess = false,
                    displayMessage = "Something went wrong!",
                    supportMessage = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] PRODUCT product)
        {
            try
            {
                APIResponseDto response = new();
                var productExists = await _context.PRODUCTS.FirstOrDefaultAsync(x => x.Id == product.Id);

                if (productExists != null)
                {
                    _context.Entry(productExists).CurrentValues.SetValues(product);
                    await _context.SaveChangesAsync();
                    response.isSuccess = true;
                    response.displayMessage = "Product updated successfully";
                    response.responseBody = product;
                    return Ok(response);
                }
                else
                {
                    response.isSuccess = false;
                    response.displayMessage = "Product not found while updating";
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                APIResponseDto response = new()
                {
                    isSuccess = false,
                    displayMessage = "Something went wrong!",
                    supportMessage = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                APIResponseDto response = new();
                var productExists = await _context.PRODUCTS.FirstOrDefaultAsync(x => x.Id == id);

                if (productExists != null)
                {
                    _context.PRODUCTS.Remove(productExists);
                    await _context.SaveChangesAsync();

                    response.isSuccess = true;
                    response.displayMessage = "Product deleted successfully";

                    return Ok(response);
                }
                else
                {
                    response.isSuccess = false;
                    response.displayMessage = $"Product wiht Id {id}, not found";

                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                APIResponseDto response = new()
                {
                    isSuccess = false,
                    displayMessage = "Something went wrong!",
                    supportMessage = ex.Message
                };

                return StatusCode(500, response);
            }
        }
    }
}
