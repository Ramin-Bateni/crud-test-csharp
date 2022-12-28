using System.Collections.Generic;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices;
using Mc2.CrudTest.ApplicationServices.IRepositories;
using Mc2.CrudTest.ApplicationServices.Models;
using Mc2.CrudTest.ApplicationServices.Services;
using NSubstitute;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.ApplicationServices.Services
{
    public class CustomerServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _repository;
        private readonly CustomerService _service;

        public CustomerServiceTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _repository = Substitute.For<ICustomerRepository>();

            _service = new CustomerService(_unitOfWork, _repository);
        }

        [Fact]
        public async Task GetAllCustomersAsync_WhenCall_ReturnsIEnumerableOfCustomers()
        {
            // Act
            var result = await _service.GetAllCustomersAsync();

            // Assert
            result.Should().BeAssignableTo<IEnumerable<Customer>>();
        }

        [Fact]
        public async Task GetCustomerByIdAsync_WhenCall_ReturnsCustomer()
        {
            // Assign
            Customer customer = new();
            _service.GetCustomerByIdAsync(1).Returns(customer);

            // Act
            var result = await _service.GetCustomerByIdAsync(1);

            // Assert
            result.Should().Be(customer);
        }
    }
}
