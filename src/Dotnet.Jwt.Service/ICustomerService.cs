using Dotnet.Jwt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dotnet.Jwt.Service
{
    public interface ICustomerService
    {
        IQueryable<Customer> Get();
    }
}
