using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Mc2.CrudTest.ApplicationServices.Models;
using Mc2.CrudTest.Domain.Customers.Commands;
using Mc2.CrudTest.Domain.Customers.Queries;
using Mc2.CrudTest.Presentation.Server.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Presentation.Server.Controllers
{
    [ExcludeFromCodeCoverage]
    public class CustomerControllerTest
    {
        private readonly CustomerController _controller;
        private readonly IMediator _mediator;
        private readonly Customer _customer;

        public CustomerControllerTest()
        {
            // A valid customer
            _customer = new()
            {
                Id = 1,
                FirstName = "Ramin",
                Lastname = "Bateni",
                PhoneNumber = "2024561111",
                Email = "a@a.com",
                BankAccountNumber = "0123456789",
                DateOfBirth = DateTime.Now,
            };
            ILogger<CustomerController> logger = Substitute.For<ILogger<CustomerController>>();
            _mediator = Substitute.For<IMediator>();

            _controller = new CustomerController(logger, _mediator);
        }

        [Fact]
        public async Task GetAll_WhenCall_ReturnsIEnumerableOfCustomer()
        {
            // Act
            IEnumerable<Customer> result = await _controller.GetAll();

            // Assert
            result.Should().BeAssignableTo<IEnumerable<Customer>>();
        }

        [Fact]
        public async Task GetById_WhenCall_ReturnsCustomer()
        {
            // Assign
            GetCustomerByIdQuery getCustomerByIdQuery = new();
            _mediator.Send(getCustomerByIdQuery).ReturnsForAnyArgs(_customer);

            // Act
            Customer result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<Customer>();
        }

        [Fact]
        public async Task CreateCustomer_WhenCustomerIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            _mediator.Send(new IsSameCustomerExistQuery()).ReturnsForAnyArgs(false);
            _mediator.Send(new IsEmailExistQuery()).ReturnsForAnyArgs(false);
            _mediator.Send(new CreateCustomerCommand()).ReturnsForAnyArgs(_customer);

            // Act
            ActionResult result = await _controller.CreateCustomer(_customer);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreateCustomer_WhenPhoneNumberIsInvalid_ReturnsBadRequestObjectResult()
        {
            // Arrange
            CreateCustomerCommand createCustomerCommand = new();
            _customer.PhoneNumber = "1"; // Invalid phone number

            _mediator.Send(createCustomerCommand).ReturnsForAnyArgs(_customer);

            // Act
            ActionResult result = await _controller.CreateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateCustomer_WhenEmailIsInvalid_ReturnsBadRequestObjectResult()
        {
            // Arrange
            CreateCustomerCommand createCustomerCommand = new();
            _customer.Email = "myname"; // Invalid email

            _mediator.Send(createCustomerCommand).ReturnsForAnyArgs(_customer);

            // Act
            ActionResult result = await _controller.CreateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateCustomer_WhenBankAccountIsInvalid_ReturnsBadRequestObjectResult()
        {
            // Arrange
            CreateCustomerCommand createCustomerCommand = new();
            _customer.BankAccountNumber = "1"; // Invalid bank account

            _mediator.Send(createCustomerCommand).ReturnsForAnyArgs(_customer);

            // Act
            ActionResult result = await _controller.CreateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateCustomer_WhenSameCustomerIsExist_ReturnsBadRequestObjectResult()
        {
            // Arrange
            CreateCustomerCommand createCustomerCommand = new();

            _mediator.Send(new IsSameCustomerExistQuery()).ReturnsForAnyArgs(true);
            _mediator.Send(createCustomerCommand).ReturnsForAnyArgs(_customer);

            // Act
            ActionResult result = await _controller.CreateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateCustomer_WhenSameEmailIsExist_ReturnsBadRequestObjectResult()
        {
            // Arrange
            CreateCustomerCommand createCustomerCommand = new();

            _mediator.Send(new IsSameCustomerExistQuery()).ReturnsForAnyArgs(false);
            _mediator.Send(new IsEmailExistQuery()).ReturnsForAnyArgs(true);
            _mediator.Send(createCustomerCommand).ReturnsForAnyArgs(_customer);

            // Act
            ActionResult result = await _controller.CreateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateCustomer_WhenCustomerIsValid_ReturnsCustomerId()
        {
            // Arrange
            _mediator.Send(new IsSameCustomerExistQuery()).ReturnsForAnyArgs(false);
            _mediator.Send(new IsEmailExistQuery()).ReturnsForAnyArgs(false);
            _mediator.Send(new UpdateCustomerCommand()).ReturnsForAnyArgs(1);

            // Act
            ActionResult result = await _controller.UpdateCustomer(_customer);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateCustomer_WhenPhoneNumberIsInvalid_ReturnsCustomerId()
        {
            // Arrange
            UpdateCustomerCommand updateCustomerCommand = new();
            _customer.PhoneNumber = "1"; // Invalid phone number

            _mediator.Send(updateCustomerCommand).ReturnsForAnyArgs(1);

            // Act
            ActionResult result = await _controller.UpdateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateCustomer_WhenEmailIsInvalid_ReturnsCustomerId()
        {
            // Arrange
            UpdateCustomerCommand updateCustomerCommand = new();
            _customer.Email = "myname"; // Invalid email

            _mediator.Send(updateCustomerCommand).ReturnsForAnyArgs(1);

            // Act
            ActionResult result = await _controller.UpdateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateCustomer_WhenBankAccountIsInvalid_ReturnsCustomerId()
        {
            // Arrange
            UpdateCustomerCommand updateCustomerCommand = new();
            _customer.Email = "myname"; // Invalid email

            _mediator.Send(updateCustomerCommand).ReturnsForAnyArgs(1);

            // Act
            ActionResult result = await _controller.UpdateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateCustomer_WhenSameCustomerIsExistIsInvalid_ReturnsCustomerId()
        {
            // Arrange
            UpdateCustomerCommand updateCustomerCommand = new();
            _mediator.Send(new IsSameCustomerExistQuery()).ReturnsForAnyArgs(true);
            _mediator.Send(updateCustomerCommand).ReturnsForAnyArgs(1);

            // Act
            ActionResult result = await _controller.UpdateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateCustomer_WhenSameEmailIsExistIsInvalid_ReturnsCustomerId()
        {
            // Arrange
            UpdateCustomerCommand updateCustomerCommand = new();
            _mediator.Send(new IsSameCustomerExistQuery()).ReturnsForAnyArgs(false);
            _mediator.Send(new IsEmailExistQuery()).ReturnsForAnyArgs(true);
            _mediator.Send(updateCustomerCommand).ReturnsForAnyArgs(1);

            // Act
            ActionResult result = await _controller.UpdateCustomer(_customer);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteCustomer_WhenCall_ReturnsCustomerId()
        {
            // Arrange
            const int id = 1;
            DeleteCustomerCommand deleteCustomerCommand = new();

            _mediator.Send(deleteCustomerCommand).ReturnsForAnyArgs(id);

            // Act
            int result = await _controller.DeleteCustomer(id);

            // Assert
            result.Should().Be(id);
        }
    }
}