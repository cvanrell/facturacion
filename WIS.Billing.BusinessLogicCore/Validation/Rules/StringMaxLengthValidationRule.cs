using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    public class StringMaxLengthValidationRule : IValidationRule
    {
        private readonly string _value;
        private readonly int _maxLength;


        public StringMaxLengthValidationRule(string value,int maxLength)
        {
            this._value = value;
            this._maxLength = maxLength;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();
            if (!string.IsNullOrEmpty(this._value))
            {
                if (this._value.Length > this._maxLength)
                    errors.Add(new ValidationError(string.Format("Largo máximo ({0}) de campo excedido", this._maxLength)));
            }            

            return errors;
        }
    }
}
