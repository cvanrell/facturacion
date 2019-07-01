using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.FilterUtil.Expressions
{
    public class ExpressionStartsWith : Expression
    {
        public ConstantExpression Content { get; set; }

        public ExpressionStartsWith(ConstantExpression expression)
        {
            this.Content = expression;
        }
    }
}
