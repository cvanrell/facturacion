using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class PositiveIntValidationRule : IValidationRule
    {
        private readonly string _value;

        public PositiveIntValidationRule(string value)
        {
            this._value = value;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            if (string.IsNullOrEmpty(this._value))
                return errors;

            if (!int.TryParse(this._value, out int parsedValue) || parsedValue < 0)
                errors.Add(new ValidationError("Valor debe ser un número entero positivo"));

            return errors;
        }
    }
}
