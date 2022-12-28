using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ApplicationServices
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
