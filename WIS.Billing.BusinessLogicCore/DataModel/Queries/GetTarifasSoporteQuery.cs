using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;
using WIS.BusinessLogicCore.DataModel.Queries;

namespace WIS.Billing.BusinessLogicCore.DataModel.Queries
{
    public class GetTarifasSoporteQuery : IQueryObject<SupportRate>
    {
        private readonly string _filtro;
        private readonly string _idCliente;

        public GetTarifasSoporteQuery(string filtro, string idCliente)
        {
            this._filtro = filtro;
            this._idCliente = idCliente;
        }

        public IQueryable<SupportRate> BuildQuery(WISDB context)
        {
            return context.SupportRates.Where(x => x.Client.Id.ToString()  == this._filtro);
        }        
    }
}
