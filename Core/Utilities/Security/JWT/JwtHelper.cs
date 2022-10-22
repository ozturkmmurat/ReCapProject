using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } // WebApi deki Appsetings okumamızı sağlıyor.
        private TokenOptions _tokenOptions; // WebAppsetings de okuduğu verileri aktardığımız Nesne
        private DateTime _accessTokenExpiration; // Topken Options ne zaman geçersizleşecek 
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); // Appsetings git Tokenoptions Section bul ve 
                                                                                          // Oradaki verileri TokenOptions ile maple yani TokenOptions daki Audience ile
                                                                                          //TokenOptions daki Audience ata 

        }
        public AccessToken CreateToken(Entities.Concrete.User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddSeconds(_tokenOptions.AccessTokenExpiration); // Şimdiye 10 dk ekle 
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);  //Securitey oluşturuyoruz 
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); // Hangi algoritmayı ve hangi anahtarı kullanayım diyor
                                                                                                     // Credentials kullanıcı bilgileri oluyor.
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = CreateRefreshToken(user, operationClaims),
                RefreshTokenEndDate = _accessTokenExpiration.AddSeconds(30)
            };

        }

        public string CreateRefreshToken(Entities.Concrete.User user, List<OperationClaim> operationClaims)
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
           
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, Entities.Concrete.User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(Entities.Concrete.User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddFirstName($"{user.FirstName}");
            claims.AddLastName($"{user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
