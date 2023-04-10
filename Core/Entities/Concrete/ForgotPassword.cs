using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class ForgotPassword : IEntity
    {
        public int Id { get; set; }
        public int ForgotPasswordCode { get; set; }
    }
}
