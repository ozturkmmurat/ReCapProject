﻿using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImagesValidator : AbstractValidator<CarImages>
    {
        public CarImagesValidator()
        {
            RuleFor(c => c.ImagePath).NotEmpty();
            RuleFor(c => c.ImagePath).MinimumLength(4);
            RuleFor(c => c.CreateDate).NotEmpty();
        }
    }
}
