using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;
using WIS.BusinessLogicCore.Validation;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    public class ProductoExistsValidationRule : IValidationRule
    {
        private readonly string _valueEmpresa;
        private readonly string _valueProducto;
        private readonly WISDB _context;

        public ProductoExistsValidationRule(WISDB context, string valueEmpresa, string valueProducto)
        {
            this._valueEmpresa = valueEmpresa;
            this._valueProducto = valueProducto;
            this._context = context;
        }

        public List<IValidationError> Validate()
        {
            var cdEmpresa = int.Parse(this._valueEmpresa);

            var errors = new List<IValidationError>();

            //if (!this._context.T_PRODUTO.Any(d => d.CD_EMPRESA == cdEmpresa && d.CD_PRODUTO == this._valueProducto))
            //    errors.Add(new ValidationError("Producto no existe"));

            return errors;
        }
    }
}
