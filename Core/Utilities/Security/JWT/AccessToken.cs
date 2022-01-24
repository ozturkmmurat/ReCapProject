using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken  // Erişim anahtarı olarak geçiyor 
    {
        public string Token { get; set; } // Jwt kendisi 
        public DateTime Expiration { get; set; } // Tokenın ne zaman sonlanacağı bilgisi
    }
}
