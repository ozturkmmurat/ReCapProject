using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class UserType :  IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
