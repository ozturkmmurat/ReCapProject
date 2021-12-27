using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcers.Validation
{
    public class ValidationTool
    {
        public static void Validate(IValidator validator , object entity)
        {
            var context = new ValidationContext<object>(entity); // Ney için doğrulama yapacaksın  
            var result = validator.Validate(context);   // Neyi Kullanacaksın, kullanacağın Validator hangi Object için yapacaksnın
            if (!result.IsValid) // Sonuç geçerli değil ise 
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
