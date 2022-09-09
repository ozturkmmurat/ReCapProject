using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class UserMail : IEntity
    {
        public string FirstNameLastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MailBody { get; set; }
    }
}
