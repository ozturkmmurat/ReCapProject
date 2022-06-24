using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentCarDto : IEntity
    {
        public Rental Rental { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
