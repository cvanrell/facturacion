using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    public class ExisteMonedaValidationRule : IValidationRule
    {
        private readonly string _valueMoneda;
        private readonly WISDB _context;

        public ExisteMonedaValidationRule(string monedaValue, WISDB context)
        {
            this._valueMoneda = monedaValue;
            this._context = context;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            //if (!this._context.T_MONEDA.Any(c => c.CD_MONEDA == this._valueMoneda))
            //    errors.Add(new ValidationError("Moneda no existe"));

            return errors;
        }
    }
}
