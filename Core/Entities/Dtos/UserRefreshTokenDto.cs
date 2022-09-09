using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dtos
{
    public class UserRefreshTokenDto : IDto
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefresTokenExpiration { get; set; }
    }
}
