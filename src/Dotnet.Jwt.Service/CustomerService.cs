using Dotnet.Jwt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet.Jwt.Service
{
    public class CustomerService : ICustomerService
    {
        public IQueryable<Customer> Get() => new List<Customer>()
        {
            new Customer()
            {
                Name = "Fulano",
                LastName = "da Silva",
                Email = "fulano@xpto.com.br",
                Password = "123456"
            },
            new Customer()
            {
                Name = "Ciclano",
                LastName = "da Silva",
                Email = "ciclano@xpto.com.br",
                Password = "123456"
            },
            new Customer()
            {
                Name = "Beltrano",
                LastName = "da Silva",
                Email = "beltrano@xpto.com.br",
                Password = "123456"
            },
        }.AsQueryable();
    }
}
