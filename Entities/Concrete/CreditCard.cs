using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public string CartNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
        public DateTime TodaysDate { set { TodaysDate = DateTime.Now; } }
    }
}
