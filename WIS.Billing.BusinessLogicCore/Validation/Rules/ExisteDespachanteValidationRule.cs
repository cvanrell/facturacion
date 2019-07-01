using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class ExisteDespachanteValidationRule : IValidationRule
    {
        private readonly string _despachanteValue;
        private readonly WISDB _context;

        public ExisteDespachanteValidationRule(string despachanteValue, WISDB context)
        {
            this._despachanteValue = despachanteValue;
            this._context = context;
        }

        public List<IValidationError> Validate()
        {
            var cdDespachante = int.Parse(this._despachanteValue);

            var errors = new List<IValidationError>();

            if (!this._context.T_DESPACHANTE.Any(d => d.CD_DESPACHANTE == cdDespachante))
                errors.Add(new ValidationError("Despachante no existe"));

            return errors;
        }
    }
}
