using CustomerWebAPI.Data;
using CustomerWebAPI.Models.Domains;
using CustomerWebAPI.Models.DTOs;
using CustomerWebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        private readonly ICustomer _customerRepo;

        public CustomerController(CustomerDbContext context, ICustomer customerRepo)
        {
            _context = context;
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customerRepo.GetAllCustomers();

                if (customers != null)
                {
                    return Ok(customers);
                }
                else
                {
                    throw new Exception();
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _context.CUSTOMERS.FirstOrDefaultAsync(x => x.Id == id);
                APIResponseDto response = new();

                if (customer == null)
                {
                    response.isSuccess = true;
                    response.displayMessage = $"Customer with Id {id}, Not found";
                    return NotFound(response);
                }
                else
                {
                    response.isSuccess = true;
                    response.displayMessage = "Customer fetched successfully";
                    response.responseBody = customer;
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
        public async Task<IActionResult> AddCustomer([FromBody] CUSTOMER customer)
        {
            try
            {
                var response = await _customerRepo.AddCustomer(customer);
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
        public async Task<IActionResult> UpdateCustomer([FromBody] CUSTOMER customer)
        {
            try
            {
                APIResponseDto response = new();
                var customerExists = await _context.CUSTOMERS.FirstOrDefaultAsync(x => x.Id == customer.Id);

                if (customerExists != null)
                {
                    _context.Entry(customerExists).CurrentValues.SetValues(customer);
                    await _context.SaveChangesAsync();
                    response.isSuccess = true;
                    response.displayMessage = "Customer updated successfully";
                    response.responseBody = customer;
                    return Ok(response);
                }
                else
                {
                    response.isSuccess = false;
                    response.displayMessage = "Customer not found while updating";
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
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                APIResponseDto response = new();
                var customerExists = await _context.CUSTOMERS.FirstOrDefaultAsync(x => x.Id == id);

                if (customerExists != null)
                {
                    _context.CUSTOMERS.Remove(customerExists);
                    await _context.SaveChangesAsync();

                    response.isSuccess = true;
                    response.displayMessage = "Customer deleted successfully";

                    return Ok(response);
                }
                else
                {
                    response.isSuccess = false;
                    response.displayMessage = $"Customer wiht Id {id}, not found";

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
