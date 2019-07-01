using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    public class ExisteTipoAlmacenajeSeguroValidationRule : IValidationRule
    {
        private readonly string _almacenajeSeguroValue;
        private readonly WISDB _context;

        public ExisteTipoAlmacenajeSeguroValidationRule(string almacenajeSeguroValue,WISDB context)
        {
            this._almacenajeSeguroValue = almacenajeSeguroValue;
            this._context = context;
        }

        public List<IValidationError> Validate()
        {
            var tpAlmacenajeSeguro = short.Parse(this._almacenajeSeguroValue);

            var errors = new List<IValidationError>();

            //if (!this._context.T_TIPO_ALMACENAJE_SEGURO.Any(a => a.TP_ALMACENAJE_Y_SEGURO == tpAlmacenajeSeguro))
            //    errors.Add(new ValidationError("Tipo de almacenaje y seguro no existe"));

            return errors;
        }
    }
}
