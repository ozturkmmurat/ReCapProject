using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public static class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
        // WebApinin gelen bir Json Web Token doğrulaamsı gerekiyor onun için bu bölüm var
        //Anahtarını ve şifreleme algoritmasını belirtiyoruz.
        //Credentials kullanıcının giriş yaparken ki bilgileri 
    }
}
