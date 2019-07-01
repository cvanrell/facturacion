using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.DataModel.Queries
{
    public class DocumentoDetalleQuery : IQueryObject<V_DOCUMENTO_DOC080>
    {
        private readonly WISDB _context;
        private readonly string NroDocumento;
        private readonly string TipoDocumento;

        public DocumentoDetalleQuery(WISDB context, string nroDocumento, string tipoDocumento)
        {
            this._context = context;
            this.NroDocumento = nroDocumento;
            this.TipoDocumento = tipoDocumento;
        }

        public IQueryable<V_DOCUMENTO_DOC080> GetQuery()
        {
            return this._context.V_DOCUMENTO_DOC080.Where(d => d.NU_DOCUMENTO == this.NroDocumento && d.TP_DOCUMENTO == this.TipoDocumento);
        }
    }
}
