using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class PositiveDecimalValidationRule : IValidationRule
    {
        private readonly string _value;

        public PositiveDecimalValidationRule(string value)
        {
            this._value = value;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            if (string.IsNullOrEmpty(this._value))
                return errors;

            if (!decimal.TryParse(this._value, out decimal parsedValue) || parsedValue < 0)
                errors.Add(new ValidationError("Valor debe ser un número positivo"));

            return errors;
        }
    }
}
