using System.Collections.Generic;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.Models;
using Mc2.CrudTest.Data;
using Mc2.CrudTest.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Data.Repository
{
    public class CustomerRepositoryTest
    {
        private readonly CustomerRepository _repo;
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepositoryTest()
        {
            _dbContext = Substitute.For<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            _repo = new CustomerRepository(_dbContext);
        }

        [Fact]
        public async Task GetAllCustomersAsync_WhenCall_ReturnsIEnumerableOfCustomers()
        {
            // Act
            var result = await _repo.GetAllCustomersAsync();

            // Assert
            result.Should().BeAssignableTo<IEnumerable<Customer>>();
        }

        [Fact]
        public async Task GetCustomerByIdAsync_WhenCall_ReturnsCustomer()
        {
            // Assign
            Customer customer = new();
            _repo.GetCustomerByIdAsync(1).Returns(customer);

            // Act
            var result = await _repo.GetCustomerByIdAsync(1);

            // Assert
            result.Should().Be(customer);
        }
    }
}
