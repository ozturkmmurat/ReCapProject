using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            // Appsetings.json daki SecurityKey byte olarak çeviriyor simetrik olarak ek oalrak bir de asimetrik var.
            // Sebebi ise Asp.net'in Json web token anlayacağı hale getirmemiz gerektiğinden.
        }
    }
}
