using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.FilterUtil.Expressions
{
    public interface IFilterExpression
    {
        Expression Evaluate(FilterExpressionContext context);
    }
}
