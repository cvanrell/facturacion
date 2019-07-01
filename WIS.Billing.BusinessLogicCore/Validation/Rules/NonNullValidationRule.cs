using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class NonNullValidationRule : IValidationRule
    {
        private readonly string _value;

        public NonNullValidationRule(string value)
        {
            this._value = value;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            if (string.IsNullOrEmpty(this._value))
                errors.Add(new ValidationError("Valor requerido"));

            return errors;
        }
    }
}
