using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.IRepositories;
using Mc2.CrudTest.ApplicationServices.Models;

namespace Mc2.CrudTest.ApplicationServices.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _repo;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository repo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        public async Task<IEnumerable<Customer>> GetCustomersListAsync()
        {
            return await _repo.GetCustomersListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _repo.GetCustomerByIdAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _repo.CreateCustomerAsync(customer);
            await _unitOfWork.SaveAsync();
            return customer;
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            return await _repo.UpdateCustomerAsync(customer);
        }

        public async Task<int> DeleteCustomerAsync(int customerId)
        {
            return await _repo.DeleteCustomerAsync(customerId);
        }
    }
}
