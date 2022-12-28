using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.Models;

namespace Mc2.CrudTest.ApplicationServices.IRepositories
{
    public interface ICustomerRepository
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
