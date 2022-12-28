using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.Services;
using MediatR;

namespace Mc2.CrudTest.Domain.Customers.Queries
{
    [ExcludeFromCodeCoverage]
    public class IsSameCustomerExistQuery : IRequest<Boolean>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public class IsSameCustomerExistQueryHandler : IRequestHandler<IsSameCustomerExistQuery, Boolean>
        {
            private readonly ICustomerService _customerService;

            public IsSameCustomerExistQueryHandler(ICustomerService customerService)
            {
                _customerService = customerService;
            }

            public async Task<bool> Handle(IsSameCustomerExistQuery query, CancellationToken cancellationToken)
            {
                return await _customerService.IsSameCustomerExistAsync(
                    query.FirstName,
                    query.LastName,
                    query.DateOfBirth);
            }
        }
    }
}
