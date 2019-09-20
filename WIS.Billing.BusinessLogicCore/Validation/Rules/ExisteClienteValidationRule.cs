using System;
using System.Collections.Generic;
using System.Text;
using WIS.Billing.DataAccessCore.Database;
using WIS.BusinessLogicCore.Validation;

namespace WIS.Billing.BusinessLogicCore.Validation.Rules
{
    public class ExisteClienteValidationRule : IValidationRule
    {
        private readonly string _idCliente;        
        private readonly WISDB _context;

        public ExisteClienteValidationRule(string idCliente, WISDB context)
        {
            this._idCliente = idCliente;            
            this._context = context;
        }

        public List<IValidationError> Validate()
        {            

            var errors = new List<IValidationError>();

            //if (!this._context.V_AGENTE.Any(d => d.CD_EMPRESA == cdEmpresa && d.CD_CLIENTE == this._valueCliente))
            //    errors.Add(new ValidationError("Empresa no existe"));

            return errors;
        }
    }
}
