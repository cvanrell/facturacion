using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.FilterUtil.Expressions
{
    public class FilterExpressionIn : IFilterExpression
    {
        private IFilterExpression ExpressionLeft { get; set; }
        private IFilterExpression ExpressionRight { get; set; }
        
        public Expression Evaluate(FilterExpressionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
