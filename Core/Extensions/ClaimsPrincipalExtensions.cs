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

        public static int GetAuthenticatedUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(result);
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role); // Claim role göre getiriyor 
        }

        //public static DateTime ClaimExpiration(this ClaimsPrincipal claimsPrincipal)
        //{
        //    var exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(claimsPrincipal.FindFirst("exp").Value));

        //    DateTime result = exp.DateTime;
        //    var dateResult = DateTime.Now - result;
        //    result.AddMilliseconds(dateResult.Milliseconds);
        //    return result;
        //}
    }
}
