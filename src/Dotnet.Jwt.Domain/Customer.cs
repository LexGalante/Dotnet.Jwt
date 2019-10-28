using System;
using System.Collections.Generic;

namespace Dotnet.Jwt.Domain
{
    public class Customer
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Role> Roles { get; set; }
    }
}
