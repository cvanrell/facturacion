using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.FilterUtil.Expressions
{
    public class FilterExpressionLiteral : IFilterExpression
    {
        private string Value { get; set; }

        public FilterExpressionLiteral(string value)
        {
            this.Value = value;
        }

        public Expression Evaluate(FilterExpressionContext context)
        {
            var prefix = this.Value.Substring(0, 1);

            if (prefix == ":")
            {
                //Es una referencia a un campo de la base
                var columnName = this.Value.Substring(1).ToUpper();

                var columnParam = context.GetType("type");

                var propertyExp = Expression.Property(columnParam, columnName);

                return propertyExp;
            }
            else
            {
                //%cosa             - EndsWith
                //cosa%             - StartsWith
                //cosa%otracosa     - StartsWith and EndsWith
                //%cosa%            - Contains

                bool hasPrefix = this.Value.Substring(0, 1) == "%";
                bool hasSuffix = this.Value.Substring(this.Value.Length - 1, 1) == "%";

                var expressionValue = Expression.Constant(this.Value.Trim('%'), typeof(string));

                if (hasPrefix && !hasSuffix)
                    return new ExpressionEndsWith(expressionValue);

                if (!hasPrefix && hasSuffix)
                    return new ExpressionStartsWith(expressionValue);

                if (hasPrefix && hasSuffix)
                    return new ExpressionContains(expressionValue);

                return expressionValue;
            }
        }
    }
}
