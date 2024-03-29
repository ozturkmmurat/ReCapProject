﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentalDetailsDto : IDto 
    {
        public int RentalId { get; set; }
        public string CarName { get; set; }
        public string CustomerName { get; set; }
        public  string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
