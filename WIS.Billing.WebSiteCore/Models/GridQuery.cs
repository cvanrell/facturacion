using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WIS.Billing.WebSiteCore.Models
{
    public class GridQuery : ObjectGraphType
    {
        public GridQuery()
        {
            /*Field<RowType>(
                "row",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> {  Name = "id" }),
                context => null
            );*/
        }
    }
}
