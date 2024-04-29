using CustomerWebAPI.Models.Domains;
using CustomerWebAPI.Models.DTOs;

namespace CustomerWebAPI.Repositories.Interfaces
{
    public interface ICustomer
    {
        Task<APIResponseDto?> GetAllCustomers();
        Task<APIResponseDto?> AddCustomer(CUSTOMER customer);
    }
}
