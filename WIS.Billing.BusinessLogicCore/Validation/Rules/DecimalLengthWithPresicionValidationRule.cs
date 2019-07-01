using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class DecimalLengthWithPresicionValidationRule : IValidationRule
    {
        private readonly string _value;
        private readonly int _largo;
        private readonly int _presicion;

        public DecimalLengthWithPresicionValidationRule(string value, int largo, int presicion)
        {
            this._value = value;
            this._largo = largo;
            this._presicion = presicion;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            string pattern = @"^-?[0-9]{0," + this._largo.ToString() + "}([.,][0-9]{0," + this._presicion.ToString() + "})?$"; // DECIMAL

            if (string.IsNullOrEmpty(this._value))
                return errors;

            bool aux = Regex.IsMatch(this._value, pattern);

            if (!aux)
            {
                errors.Add(new ValidationError("Formato incorrecto"));
            }
            else
            {
                aux = decimal.TryParse(this._value, out decimal outValue);

                if (!aux)
                    errors.Add(new ValidationError("Error en conversión"));
            }

            return errors;
        }
    }
}
