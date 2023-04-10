using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(Entities.Concrete.User user, List<OperationClaim> operationClaims);
        string CreateRefreshToken();
        
    }
}
