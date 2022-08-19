using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType) // ClaimPrincipal o an ki  kişinin jsonwebtokenla gelen bir kişinin Claimlerine ulaşmak için
                                                                                                  // Erişmek için olan class 
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList(); // ? null olabilir anlamıne geliyor ilgili claimtype göre geitriyor
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role); // Claim role göre getiriyor 
        }
    }
}
