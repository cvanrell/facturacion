using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class ExisteViaActivaValidationRule : IValidationRule
    {
        private readonly string _valueVia;
        private readonly WISDB _context;

        public ExisteViaActivaValidationRule(string valueVia, WISDB context)
        {
            this._valueVia = valueVia;
            this._context = context;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            if (!this._context.T_VIA.Any(d => d.CD_VIA == this._valueVia))
                errors.Add(new ValidationError("Via no existe"));

            return errors;
        }
    }
}
