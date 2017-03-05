using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EFEndToEnd.Business.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]  
    public class DropDownRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        private string _message;

        public DropDownRequiredAttribute(string message)
        {
            _message = message;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult returnValue = ValidationResult.Success;

            if (value is int)
            {
                if (((int)value) <= 0)
                {
                    returnValue = new ValidationResult(_message);
                }
            }
            else
            {
                returnValue = new ValidationResult(_message);
            }
            return returnValue;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = _message,
                ValidationType = "dropdownrequired"
            };
            yield return rule; 
        }
    }
}
