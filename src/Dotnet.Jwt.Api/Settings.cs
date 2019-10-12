using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Jwt.Api
{
    public class Settings
    {
        public string Secret { get; set; }
        public bool SaveToken { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string[] ValidAudiences { get; set; }
        public string[] ValidIssuers { get; set; }

        public SymmetricSecurityKey GetCryptedSecret() => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)); 
    }
}
