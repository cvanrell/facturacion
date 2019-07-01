using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.DataModel.Mappers;
using WIS.BusinessLogic.ControlDocumental;
using WIS.BusinessLogic.ControlDocumental.Enums;
using WIS.Persistance.Database;
using WIS.Persistance.Extensions;

namespace WIS.BusinessLogic.DataModel.Repositories
{
    public class DocumentoRepository
    {
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;
        private readonly DocumentoMapper _mapper;

        public DocumentoRepository(WISDB context, string application, int userId)
        {
            this._context = context;
            this._application = application;
            this._userId = userId;
            this._mapper = new DocumentoMapper();
        }

        [Obsolete]
        public IDocumento Get(string nroDocumento, string tipoDocumento)
        {
            var documentoEntity = this._context.T_DOCUMENTO.Include("T_DET_DOCUMENTO")
                .Where(d => d.NU_DOCUMENTO == nroDocumento && d.TP_DOCUMENTO == tipoDocumento).AsNoTracking().FirstOrDefault();

            return this._mapper.MapToDocumento(documentoEntity);
        }
        public IDocumentoIngreso GetIngreso(string nroDocumento, string tipoDocumento)
        {
            var documentoEntity = this._context.T_DOCUMENTO.Include("T_DET_DOCUMENTO")
                .Where(d => d.NU_DOCUMENTO == nroDocumento && d.TP_DOCUMENTO == tipoDocumento).AsNoTracking().FirstOrDefault();            

            return this._mapper.MapToIngreso(documentoEntity);
        }
        public void UpdateIngreso(IDocumentoIngreso documento)
        {
            var documentoEntity = this._mapper.MapFromIngreso(documento);

            documentoEntity.VL_DATO_AUDITORIA = string.Format("{0}${1}${2}", this._application, this._userId, this.GetTransactionId().ToString());

            this._context.T_DOCUMENTO.Attach(documentoEntity);

            this._context.Entry(documentoEntity).State = System.Data.Entity.EntityState.Modified;
        }
        public void AddIngreso(IDocumentoIngreso documento)
        {
            var documentoEntity = this._mapper.MapFromIngreso(documento);

            documentoEntity.VL_DATO_AUDITORIA = string.Format("{0}${1}${2}", this._application, this._userId, this.GetTransactionId().ToString());

            this._context.T_DOCUMENTO.Attach(documentoEntity);

            this._context.Entry(documentoEntity).State = System.Data.Entity.EntityState.Added;
        }

        public void UpdateDetail(IDocumento documento, DocumentoLinea linea)
        {
            var detailEntity = this._mapper.MapFromDocumentoLinea(documento.Numero, documento.Tipo, linea);

            this._context.T_DET_DOCUMENTO.Attach(detailEntity);

            this._context.Entry(detailEntity).State = System.Data.Entity.EntityState.Modified;
        }


        public IDocumentoEgreso GetEgreso(string nroDocumento, string tipoDocumento)
        {
            var documentoEntity = this._context.T_DOCUMENTO.Include("T_DET_DOCUMENTO")
                .Where(d => d.NU_DOCUMENTO == nroDocumento && d.TP_DOCUMENTO == tipoDocumento).AsNoTracking().FirstOrDefault();

            return this._mapper.MapToEgreso(documentoEntity);
        }
        public void UpdateEgreso(IDocumentoEgreso documento)
        {
            var documentoEntity = this._mapper.MapFromEgreso(documento);

            documentoEntity.VL_DATO_AUDITORIA = string.Format("{0}${1}${2}", this._application, this._userId, this.GetTransactionId().ToString());

            this._context.T_DOCUMENTO.Attach(documentoEntity);

            this._context.Entry(documentoEntity).State = System.Data.Entity.EntityState.Modified;
        }

        public IDocumentoActa GetActa(string nroDocumento, string tipoDocumento)
        {
            var documentoEntity = this._context.T_DOCUMENTO.Include("T_DET_DOCUMENTO")
                .Where(d => d.NU_DOCUMENTO == nroDocumento && d.TP_DOCUMENTO == tipoDocumento).AsNoTracking().FirstOrDefault();

            return this._mapper.MapToActa(documentoEntity);
        }
        public void UpdateActa(IDocumentoActa documento)
        {
            var documentoEntity = this._mapper.MapFromActa(documento);

            documentoEntity.VL_DATO_AUDITORIA = string.Format("{0}${1}${2}", this._application, this._userId, this.GetTransactionId().ToString());

            this._context.T_DOCUMENTO.Attach(documentoEntity);

            this._context.Entry(documentoEntity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Add(IDocumento documento)
        {
            var documentoEntity = this._mapper.MapFromDocumento(documento);

            this._context.T_DOCUMENTO.Attach(documentoEntity);

            this._context.Entry(documentoEntity).State = System.Data.Entity.EntityState.Added;
        }
        public void AddDetail(IDocumento documento, DocumentoLinea linea)
        {
            var detailEntity = this._mapper.MapFromDocumentoLinea(documento.Numero, documento.Tipo, linea);

            this._context.T_DET_DOCUMENTO.Add(detailEntity);
        }

        public void RemoveDetail(IDocumento documento, DocumentoLinea linea)
        {
            var detailEntity = this._mapper.MapFromDocumentoLinea(documento.Numero, documento.Tipo, linea);

            this._context.T_DET_DOCUMENTO.Attach(detailEntity);

            this._context.T_DET_DOCUMENTO.Remove(detailEntity);
        }

        public string GetNumeroDocumentoIngreso()
        {
            string query = "SELECT S_ZFM_INGRESO.nextval FROM DUAL";
            return _context.Database.SqlQuery<int>(query).FirstOrDefault().ToString();
        }
        public string GetNumeroDocumentoIngresoNacional()
        {
            string query = "SELECT S_ZFM_INGRESO_NACIONALIZACION.nextval FROM DUAL";
            return _context.Database.SqlQuery<int>(query).FirstOrDefault().ToString();
        }

        public List<EstadoDocumentoIngreso> GetEstadosParaCambioDocumentoIngreso(EstadoDocumentoIngreso estadoDocumento,string tpDocumento)
        {
            var estadoString = this._mapper.GetEstadoIngresoId(estadoDocumento);

            var ordenActual = this.GetEstadoActual(tpDocumento, estadoString);

            var estados = new List<EstadoDocumentoIngreso>();

            if (ordenActual != null)
            {
                var result = this.GetEstadosTipoDocumento(ordenActual);

                foreach (var estado in result)
                {
                    estados.Add(this._mapper.GetEstadoIngreso(estado));
                }
            }

            return estados;
        }
        public List<EstadoDocumentoEgreso> GetEstadosParaCambioDocumentoEgreso(EstadoDocumentoEgreso estadoDocumento, string tpDocumento)
        {
            var estadoString = this._mapper.GetEstadoEgresoId(estadoDocumento);

            var ordenActual = this.GetEstadoActual(tpDocumento, estadoString);

            var estados = new List<EstadoDocumentoEgreso>();

            if (ordenActual != null)
            {
                var result = this.GetEstadosTipoDocumento(ordenActual);

                foreach (var estado in result)
                {
                    estados.Add(this._mapper.GetEstadoEgreso(estado));
                }
            }

            return estados;
        }
        public List<EstadosDocumentosActa> GetEstadosParaCambioDocumentoActa(EstadosDocumentosActa estadoDocumento, string tpDocumento)
        {
            var estadoString = this._mapper.GetEstadoActaId(estadoDocumento);

            var ordenActual = this.GetEstadoActual(tpDocumento, estadoString);

            var estados = new List<EstadosDocumentosActa>();

            if (ordenActual != null)
            {
                var result = this.GetEstadosTipoDocumento(ordenActual);

                foreach (var estado in result)
                {
                    estados.Add(this._mapper.GetEstadoActa(estado));
                }
            }

            return estados;
        }

        private T_DOCUMENTO_ESTADO_ORDEN GetEstadoActual(string tpDocumento, string estado)
        {
            return _context.T_DOCUMENTO_ESTADO_ORDEN.Where(o => o.TP_DOCUMENTO == tpDocumento && o.ID_ESTADO == estado).FirstOrDefault();
        }
        private List<string> GetEstadosTipoDocumento(T_DOCUMENTO_ESTADO_ORDEN ordenActual)
        {
            return _context.T_DOCUMENTO_ESTADO_ORDEN.Where(o => o.TP_DOCUMENTO == ordenActual.TP_DOCUMENTO &&
                                                ((o.VL_ORDEN == ordenActual.VL_ORDEN + 1 ||
                                                  o.ID_ESTADO == ordenActual.ID_ESTADO_RETROCESO)) &&
                                                  ordenActual.FL_AVANCE == "S")
                                            .Select(o => o.ID_ESTADO)
                                            .ToList();
        }
        private decimal GetTransactionId()
        {
            return this._context.GetNextSequenceValue<decimal>("S_TRANSACCION");
        }
    }
}
