using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Jwt.Api
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Role> Roles { get; set; };


        public static Usuario GetUserFake(string login, string password) => new Usuario()
        {
            Id = 1,
            Login = "admin",
            Password = "xpto",
            Name = "administrador",
            Email = "admin@admin.com.br",
            Roles = new List<Role>()
            {
                new Role()
                {
                    Name = "admin"
                }
            }
        };
    }
}
