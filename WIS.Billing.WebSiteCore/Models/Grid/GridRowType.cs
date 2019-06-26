using WIS.CommonCore.GridComponents;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WIS.Billing.WebSiteCore.Models
{
    public class RowType : ObjectGraphType<GridRow>
    {
        public RowType()
        {
            Field(d => d.Id);
        }
    }
}
