using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CreditCartValidator : AbstractValidator<CreditCard>
    {
        public CreditCartValidator()
        {
            RuleFor(c => c.CartNumber).NotEmpty();
        }
    }
}
