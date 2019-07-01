using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.FilterUtil.Expressions
{
    public class FilterExpressionNot : IFilterExpression
    {
        private IFilterExpression ExpressionLeft { get; set; }

        public FilterExpressionNot(IFilterExpression expression)
        {
            this.ExpressionLeft = expression;
        }

        public Expression Evaluate(FilterExpressionContext context)
        {
            return Expression.Not(this.ExpressionLeft.Evaluate(context));
        }
    }
}
