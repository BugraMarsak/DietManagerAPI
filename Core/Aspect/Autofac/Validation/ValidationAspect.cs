using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect : MethodInterception // Aspect =hata verdiğinde çalışacak yapı
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive codding(korumacı, kısıtlama)
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)// validation için mantıklı
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//reflection çalışma anında bir şeyleri  örn:productvalidator u newlemil gibi oluyor
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//çalışma tipini bul ve generic typelerı bul // productValidator un genericin 1. değerini al
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
