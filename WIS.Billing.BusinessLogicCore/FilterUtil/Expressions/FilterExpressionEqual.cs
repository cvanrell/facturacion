using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.FilterUtil.Expressions
{
    public class FilterExpressionEqual : IFilterExpression
    {
        private IFilterExpression ExpressionLeft { get; set; }
        private IFilterExpression ExpressionRight { get; set; }

        public FilterExpressionEqual(IFilterExpression expressionLeft, IFilterExpression expressionRight)
        {
            this.ExpressionLeft = expressionLeft;
            this.ExpressionRight = expressionRight;
        }

        public Expression Evaluate(FilterExpressionContext context)
        {
            return this.ResolveOperation(this.ExpressionLeft.Evaluate(context), this.ExpressionRight.Evaluate(context));
        }

        private Expression ResolveOperation(Expression expressionLeft, Expression expressionRight)
        {
            if (expressionRight is ExpressionStartsWith startsWith)
                return ExpressionExtension.StringStartsWith(expressionLeft, startsWith.Content);

            if (expressionRight is ExpressionEndsWith endsWith)
                return ExpressionExtension.StringEndsWith(expressionLeft, endsWith.Content);

            if (expressionRight is ExpressionContains contains)
                return ExpressionExtension.StringContains(expressionLeft, contains.Content);

            return ExpressionExtension.Equals(expressionLeft, expressionRight);
        }
    }
}
