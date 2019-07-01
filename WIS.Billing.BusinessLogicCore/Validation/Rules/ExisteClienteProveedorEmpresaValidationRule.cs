using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    public class ExisteClienteProveedorEmpresaValidationRule : IValidationRule
    {
        private readonly string _valueCliente;
        private readonly string _valueEmpresa;
        private readonly WISDB _context;

        public ExisteClienteProveedorEmpresaValidationRule(string valueCliente, string valueEmpresa, WISDB context)
        {
            this._valueCliente = valueCliente;
            this._valueEmpresa = valueEmpresa;
            this._context = context;
        }

        public List<IValidationError> Validate()
        {
            var cdEmpresa = int.Parse(this._valueEmpresa);

            var errors = new List<IValidationError>();

            //if (!this._context.V_AGENTE.Any(d => d.CD_EMPRESA == cdEmpresa && d.CD_CLIENTE == this._valueCliente))
            //    errors.Add(new ValidationError("Empresa no existe"));

            return errors;
        }
    }
}
