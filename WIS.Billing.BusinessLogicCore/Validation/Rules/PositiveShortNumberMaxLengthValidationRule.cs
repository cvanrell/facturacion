using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    public class PositiveShortNumberMaxLengthValidationRule : IValidationRule
    {
        private readonly string _value;
        private readonly int _maxLength;

        public PositiveShortNumberMaxLengthValidationRule(string value, int maxLength)
        {
            this._value = value;
            this._maxLength = maxLength;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            string pattern = @"^\d{1," + this._maxLength + "}$"; // solo enteros

            if (string.IsNullOrEmpty(this._value))
                return errors;

            if (this._value.Length > this._maxLength)
                errors.Add(new ValidationError(string.Format("Largo máximo ({0}) de campo excedido", this._maxLength)));

            bool aux = Regex.IsMatch(this._value, pattern);

            if (!aux)
                errors.Add(new ValidationError("Formato incorrecto"));

            if (aux)
            {
                aux = short.TryParse(this._value, out short outValue);

                if (!aux)
                    errors.Add(new ValidationError("Error en conversión"));
            }

            return errors;
        }
    }
}
