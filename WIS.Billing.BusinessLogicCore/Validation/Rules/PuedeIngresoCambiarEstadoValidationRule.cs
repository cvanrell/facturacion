using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.DataModel;
using WIS.BusinessLogic.ControlDocumental;
using WIS.BusinessLogic.ControlDocumental.Enums;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class PuedeIngresoCambiarEstadoValidationRule : IValidationRule
    {
        private readonly string _nuDocumentoValue;
        private readonly string _tpDocumentoValue;
        private readonly EstadoDocumentoIngreso _nuevoEstadoValue;
        private readonly int _userId;

        public PuedeIngresoCambiarEstadoValidationRule(string nuDocumento, string tpDocumento, EstadoDocumentoIngreso nuevoEstado, int userId)
        {
            this._nuDocumentoValue = nuDocumento;
            this._tpDocumentoValue = tpDocumento;
            this._nuevoEstadoValue = nuevoEstado;
            this._userId = userId;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            using (UnitOfWork _context = new UnitOfWork("", this._userId))
            {
                IDocumentoIngreso documento = _context.DocumentoRepository.GetIngreso(this._nuDocumentoValue, this._tpDocumentoValue);

                List<EstadoDocumentoIngreso> estadosHabiltiados = documento.GetEstadosHabilitadosParaCambio(_context.DocumentoRepository, documento.Estado);

                if (!estadosHabiltiados.Contains(this._nuevoEstadoValue))
                    errors.Add(new ValidationError("Cambio de estado no permitido"));

                if(this._nuevoEstadoValue == EstadoDocumentoIngreso.VerificacionIniciada && documento.Agenda == null)
                    errors.Add(new ValidationError("Documento sin agenda asociada"));

                if (this._nuevoEstadoValue == EstadoDocumentoIngreso.EnviadoAduana && documento.Lineas.Count == 0)
                    errors.Add(new ValidationError("Documento sin detalles"));

            }

            return errors;
        }
    }
}
