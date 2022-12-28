using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.IRepositories;
using Mc2.CrudTest.ApplicationServices.Models;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Data.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersListAsync()
        {
            var items= await _context.Customer.ToListAsync();
            return items;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customer
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            _context.Customer.Update(customer);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteCustomerAsync(int customerId)
        {
            Customer customer = await GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return default;
            }
            _context.Customer.Remove(customer);
            return await _context.SaveChangesAsync();
        }

        public Task<bool> IsSameCustomerExistAsync(string firstName, string lastName, DateTime dateOfBirth)
        {
            return _context.Customer.AnyAsync(x => x.FirstName.ToLower() == firstName.ToLower() &&
                                         x.Lastname.ToLower() == lastName.ToLower() &&
                                         x.DateOfBirth == dateOfBirth);
        }

        public Task<bool> IsEmailExistAsync(string email)
        {
            return _context.Customer.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
