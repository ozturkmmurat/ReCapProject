using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Linq;

namespace Core.Aspects.Autofac.Validation
{

    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil.");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //  Reflection ile instance oluşturuluyor
            // parametre olarak attribute gelen instance oluşturuyor 
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //  Validation Basetype git onun  generic argümanını bul 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // İlgili metodun parametlerini bul
            // METODUN parametrelerine bak entitType denk gelen validator(FluentValidation bölümünü kast ediyor validator derken)
            //tipine eşit olanı bul  örneğin Product ile çalışıyor Product ile çalışan validationa gidip Car verirsen hata verir
            // Biribiryle uyuşması gerek generic bölümün onu sağlıyor bu bölüm 
            // isek product bul  diyor 
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); // 
            }
        }
    }
}

