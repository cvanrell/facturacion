using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.ControlDocumental.Enums;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class TipoDocumentoIngresoPermiteAltaManual : IValidationRule
    {
        private readonly string _tipoValue;

        public TipoDocumentoIngresoPermiteAltaManual(string tipoValue)
        {
            this._tipoValue = tipoValue;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            if(this._tipoValue != TipoDocumento.IngresoNacional.ToString() && this._tipoValue != TipoDocumento.Ingreso.ToString())
            {
                errors.Add(new ValidationError("Tipo de documento de ingreso no válido"));
            }

            return errors;
        }
    }
}
