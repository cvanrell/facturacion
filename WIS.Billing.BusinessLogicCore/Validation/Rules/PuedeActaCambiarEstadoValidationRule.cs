using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.ControlDocumental.Enums;
using WIS.BusinessLogicCore.DataModel;

namespace WIS.BusinessLogicCore.Validation.Rules
{
    public class PuedeActaCambiarEstadoValidationRule : IValidationRule
    {
        private readonly string _nuDocumentoValue;
        private readonly string _tpDocumentoValue;
        private readonly EstadosDocumentosActa _nuevoEstadoValue;
        private readonly int _userId;

        public PuedeActaCambiarEstadoValidationRule(string nuDocumento, string tpDocumento, EstadosDocumentosActa nuevoEstado, int userId)
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
                //IDocumentoEgreso documento = _context.DocumentoRepository.GetEgreso(this._nuDocumentoValue, this._tpDocumentoValue);

                //List<EstadosDocumentosEgreso> estadosHabiltiados = documento.GetEstadosHabilitadosParaCambio(_context.DocumentoRepository, documento.Estado);

                //if (!estadosHabiltiados.Contains(this._nuevoEstadoValue))
                //    errors.Add(new ValidationError("Cambio de estado no permitido"));
            }

            return errors;
        }
    }
}
