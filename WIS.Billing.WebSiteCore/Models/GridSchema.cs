using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WIS.Billing.WebSiteCore.Models
{
    public class GridSchema : Schema
    {
        public GridSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<GridQuery>();
            Mutation = resolver.Resolve<GridMutation>();
        }
    }
}
