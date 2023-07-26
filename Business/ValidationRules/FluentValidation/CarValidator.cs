using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
      public CarValidator()
        {
            RuleFor(r => r.DailyPrice).GreaterThan(0);
            RuleFor(r => r.Description).MinimumLength(2);
            RuleFor(r => r.Name).MinimumLength(2);
        }
    }
}
