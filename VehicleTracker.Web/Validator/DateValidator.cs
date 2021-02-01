using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleTracker.Web.Validator
{

    public class ValidateDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt;
            bool parsed = DateTime.TryParse((string)value, out dt);
            if (!parsed)
                return false;



            return true;
        }

    }
    public class DateLessThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public DateLessThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            ErrorMessage = ErrorMessageString;
            var currentValue = (DateTime?)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);
            if (currentValue.HasValue && comparisonValue.HasValue)
                if (currentValue.Value > comparisonValue.Value)
                    return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
