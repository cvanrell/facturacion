using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    public class ExisteTransportadoraValidationRule : IValidationRule
    {
        private readonly string _valueTransportadora;
        private readonly WISDB _context;

        public ExisteTransportadoraValidationRule(string valueTransportadora, WISDB context)
        {
            this._valueTransportadora = valueTransportadora;
            this._context = context;
        }

        public List<IValidationError> Validate()
        {
            var cdTransportadora = int.Parse(this._valueTransportadora);

            var errors = new List<IValidationError>();

            //if (!this._context.T_TRANSPORTADORA.Any(t => t.CD_TRANSPORTADORA == cdTransportadora))
            //    errors.Add(new ValidationError("Transportista no existe"));

            return errors;
        }
    }
}
