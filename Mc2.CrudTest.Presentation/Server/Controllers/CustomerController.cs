using System.Collections.Generic;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.Models;
using Mc2.CrudTest.Domain.Customers.Commands;
using Mc2.CrudTest.Domain.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            IEnumerable<Customer> result = await _mediator.Send(new GetAllCustomersQuery());
            return result;
        }

        [HttpGet]
        [Route(nameof(GetById))]
        public async Task<Customer> GetById(int id)
        {
            return await _mediator.Send(new GetCustomerByIdQuery() { Id = id });
        }

        [HttpPost]
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            customer = await _mediator.Send(new CreateCustomerCommand()
            {
                FirstName = customer.FirstName,
                Lastname = customer.Lastname,
                Email = customer.Email,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber,
                BankAccountNumber = customer.BankAccountNumber
            });
            return customer;
        }

        [HttpPut]
        public async Task<int> UpdateCustomer(Customer customer)
        {
            int id = await _mediator.Send(new UpdateCustomerCommand()
            {
                Id=customer.Id,
                FirstName = customer.FirstName,
                Lastname = customer.Lastname,
                Email = customer.Email,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber,
                BankAccountNumber = customer.BankAccountNumber
            });
            return id;
        }

        [HttpDelete]
        public async Task<int> DeleteCustomer(int customerId)
        {
            int id = await _mediator.Send(new DeleteCustomerCommand()
            {
                Id = customerId
            });
            return id;
        }
    }
}
