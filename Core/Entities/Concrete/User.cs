using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt  { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Biography { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
