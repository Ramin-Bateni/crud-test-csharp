﻿using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.Models;
using Mc2.CrudTest.ApplicationServices.Services;
using MediatR;

namespace Mc2.CrudTest.Domain.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
        {
            private readonly ICustomerService _customerService;

            public DeleteCustomerCommandHandler(ICustomerService customerService)
            {
                _customerService = customerService;
            }

            public async Task<int> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
            {
                return await _customerService.DeleteCustomerAsync(command.Id);
            }
        }
    }
}
