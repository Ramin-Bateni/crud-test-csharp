using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationServices.Services;
using MediatR;

namespace Mc2.CrudTest.Domain.Customers.Queries
{
    [ExcludeFromCodeCoverage]
    public class IsEmailExistQuery : IRequest<bool>
    {
        public string Email { get; set; }

        public class IsEmailExistQueryHandler : IRequestHandler<IsEmailExistQuery, bool>
        {
            private readonly ICustomerService _customerService;

            public IsEmailExistQueryHandler(ICustomerService customerService)
            {
                _customerService = customerService;
            }

            public async Task<bool> Handle(IsEmailExistQuery query, CancellationToken cancellationToken)
            {
                return await _customerService.IsEmailExistAsync(query.Email);
            }
        }
    }
}
