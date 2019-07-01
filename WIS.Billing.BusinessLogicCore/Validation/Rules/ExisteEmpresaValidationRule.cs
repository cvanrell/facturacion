using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    class ExisteEmpresaValidationRule : IValidationRule
    {
        private readonly string _valueEmpresa;
        private readonly WISDB _context;

        public ExisteEmpresaValidationRule(string valueEmpresa, WISDB context)
        {
            this._valueEmpresa = valueEmpresa;
            this._context = context;
        }

        public List<IValidationError> Validate()
        {
            var cdEmpresa = int.Parse(this._valueEmpresa);

            var errors = new List<IValidationError>();

            //if (!this._context.T_EMPRESA.Any(d => d.CD_EMPRESA == cdEmpresa))
            //    errors.Add(new ValidationError("Empresa no existe"));

            return errors;
        }
    }
}
