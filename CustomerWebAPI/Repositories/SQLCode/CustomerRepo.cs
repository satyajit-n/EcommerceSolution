using CustomerWebAPI.Data;
using CustomerWebAPI.Models.Domains;
using CustomerWebAPI.Models.DTOs;
using CustomerWebAPI.RabbitMQ;
using CustomerWebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebAPI.Repositories.SQLCode
{
    public class CustomerRepo : ICustomer
    {
        private readonly CustomerDbContext _context;
        private readonly IMessageProducer _messageProducer;

        public CustomerRepo(CustomerDbContext context,IMessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
        }

        public async Task<APIResponseDto?> GetAllCustomers()
        {
            try
            {
                var customers = await _context.CUSTOMERS.ToListAsync();

                APIResponseDto response = new()
                {
                    isSuccess = true,
                    displayMessage = "Customers data fetched successfully",
                    responseBody = customers,
                    supportMessage = null
                };
                _messageProducer?.SendMessage(response);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return null;
            }
        }

        public async Task<APIResponseDto?> AddCustomer(CUSTOMER customer)
        {
            try
            {
                APIResponseDto response = new();
                await _context.CUSTOMERS.AddAsync(customer);
                await _context.SaveChangesAsync();

                response.isSuccess = true;
                response.displayMessage = "Customer added successfully";
                response.responseBody = customer;

                _messageProducer?.SendMessage(response);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
