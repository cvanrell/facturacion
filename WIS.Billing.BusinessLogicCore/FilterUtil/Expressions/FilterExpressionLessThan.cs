using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.FilterUtil.Expressions
{
    public class FilterExpressionLessThan : IFilterExpression
    {
        private IFilterExpression ExpressionLeft { get; set; }
        private IFilterExpression ExpressionRight { get; set; }

        public FilterExpressionLessThan(IFilterExpression expressionLeft, IFilterExpression expressionRight)
        {
            this.ExpressionLeft = expressionLeft;
            this.ExpressionRight = expressionRight;
        }

        public Expression Evaluate(FilterExpressionContext context)
        {
            return ExpressionExtension.LessThan(this.ExpressionLeft.Evaluate(context), this.ExpressionRight.Evaluate(context));
        }
    }
}
