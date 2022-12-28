using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.Models;

namespace Mc2.CrudTest.ApplicationServices.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersListAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<int> UpdateCustomerAsync(Customer customer);
        Task<int> DeleteCustomerAsync(int customerId);
        Task<bool> IsSameCustomerExistAsync(string firstName, string lastName, DateTime dateOfBirth);
        Task<bool> IsEmailExistAsync(string email);
    }
}
