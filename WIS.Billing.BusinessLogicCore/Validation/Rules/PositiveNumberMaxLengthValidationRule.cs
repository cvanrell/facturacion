using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class PositiveNumberMaxLengthValidationRule : IValidationRule
    {
        private readonly string _value;
        private readonly int _maxLength;

        public PositiveNumberMaxLengthValidationRule(string value,int maxLength)
        {
            this._value = value;
            this._maxLength = maxLength;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            bool aux = false;
            string pattern = @"^\d{1,10}$"; // solo enteros

            if (string.IsNullOrEmpty(this._value))
                return errors;

            if (this._value.Length > this._maxLength)
                errors.Add(new ValidationError(string.Format("Largo máximo ({0}) de campo excedido",this._maxLength)));

            aux = Regex.IsMatch(this._value, pattern);
            if (!aux)
                errors.Add(new ValidationError("Formato incorrecto"));

            if (aux)
            {
                int outValue;
                aux = Int32.TryParse(this._value, out outValue);
                if (!aux)
                    errors.Add(new ValidationError("Error en conversión"));
            }

            return errors;
        }
    }
}
