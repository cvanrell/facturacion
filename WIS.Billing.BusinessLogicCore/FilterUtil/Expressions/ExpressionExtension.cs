using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.FilterUtil.Expressions
{
    public class ExpressionExtension
    {
        public static Expression StringContains(Expression paramExpLeft, Expression paramExpRight)
        {
            MethodInfo methodContains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });

            MethodCallExpression paramExpLeftLower = Expression.Call(paramExpLeft, "ToLower", null);
            MethodCallExpression paramExpRightLower = Expression.Call(paramExpRight, "ToLower", null);

            return Expression.Call(paramExpLeftLower, methodContains, paramExpRightLower);
        }
        public static Expression StringStartsWith(Expression paramExpLeft, Expression paramExpRight)
        {
            MethodInfo methodStartsWith = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            
            MethodCallExpression paramExpLeftLower = Expression.Call(paramExpLeft, "ToLower", null);
            MethodCallExpression paramExpRightLower = Expression.Call(paramExpRight, "ToLower", null);

            return Expression.Call(paramExpLeftLower, methodStartsWith, paramExpRightLower);
        }
        public static Expression StringEndsWith(Expression paramExpLeft, Expression paramExpRight)
        {
            MethodInfo methodEndsWith = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

            MethodCallExpression paramExpLeftLower = Expression.Call(paramExpLeft, "ToLower", null);
            MethodCallExpression paramExpRightLower = Expression.Call(paramExpRight, "ToLower", null);

            return Expression.Call(paramExpLeftLower, methodEndsWith, paramExpRightLower);
        }

        public static Expression Equals(Expression paramExpLeft, Expression paramExpRight)
        {
            ResolveType(paramExpLeft, paramExpRight, out Expression convertedLeft, out Expression convertedRight);

            if(convertedLeft.Type == typeof(string))
            {
                convertedLeft = Expression.Call(paramExpLeft, "ToLower", null);
                convertedRight = Expression.Call(paramExpRight, "ToLower", null);
            }

            return Expression.MakeBinary(ExpressionType.Equal, convertedLeft, convertedRight);
        }
        public static Expression GreaterThan(Expression paramExpLeft, Expression paramExpRight)
        {
            ResolveType(paramExpLeft, paramExpRight, out Expression convertedLeft, out Expression convertedRight);

            return Expression.MakeBinary(ExpressionType.GreaterThan, convertedLeft, convertedRight);
        }
        public static Expression GreaterThanOrEqual(Expression paramExpLeft, Expression paramExpRight)
        {
            ResolveType(paramExpLeft, paramExpRight, out Expression convertedLeft, out Expression convertedRight);

            return Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, convertedLeft, convertedRight);
        }
        public static Expression LessThan(Expression paramExpLeft, Expression paramExpRight)
        {
            ResolveType(paramExpLeft, paramExpRight, out Expression convertedLeft, out Expression convertedRight);

            return Expression.MakeBinary(ExpressionType.LessThan, convertedLeft, convertedRight);
        }
        public static Expression LessThanOrEqual(Expression paramExpLeft, Expression paramExpRight)
        {
            ResolveType(paramExpLeft, paramExpRight, out Expression convertedLeft, out Expression convertedRight);

            return Expression.MakeBinary(ExpressionType.LessThanOrEqual, convertedLeft, convertedRight);
        }

        private static void ResolveType(Expression expressionLeft, Expression expressionRight, out Expression convertedLeft, out Expression convertedRight)
        {
            convertedLeft = expressionLeft;
            convertedRight = expressionRight;

            if (expressionLeft is MemberExpression mExpL && expressionRight is ConstantExpression cExpR)
                convertedRight = ConvertType(cExpR, mExpL.Type);

            if (expressionRight is MemberExpression mExpR && expressionLeft is ConstantExpression cExpL)
                convertedLeft = ConvertType(cExpL, mExpR.Type);
        }
        private static Expression ConvertType(ConstantExpression expression, Type type)
        {
            string value = (string)expression.Value;

            if (type == typeof(string))
                return expression;

            if (type == typeof(int))
                return Expression.Convert(Expression.Constant(int.Parse(value)), typeof(int));

            if (type == typeof(int?))
                return Expression.Convert(Expression.Constant(int.Parse(value)), typeof(int?));

            if (type == typeof(decimal))
                return Expression.Convert(Expression.Constant(decimal.Parse(value)), typeof(decimal));

            if (type == typeof(decimal?))
                return Expression.Convert(Expression.Constant(decimal.Parse(value)), typeof(decimal?));

            if (type == typeof(short))
                return Expression.Convert(Expression.Constant(short.Parse(value)), typeof(short));

            if (type == typeof(short?))
                return Expression.Convert(Expression.Constant(short.Parse(value)), typeof(short?));

            if (type == typeof(long))
                return Expression.Convert(Expression.Constant(long.Parse(value)), typeof(long));

            if (type == typeof(long?))
                return Expression.Convert(Expression.Constant(long.Parse(value)), typeof(long?));

            if (type == typeof(DateTime))
            {
                var ci = new CultureInfo("es-UY");
                var formatString = "dd/MM/yyyy HH:mm:ss"; //TODO: Pasar a constante

                if (value.ToString().Length == 10)
                    formatString = "dd/MM/yyyy";

                return Expression.Convert(Expression.Constant(DateTime.ParseExact(value, formatString, ci)), typeof(DateTime));
            }

            if (type == typeof(DateTime?))
            {
                var ci = new CultureInfo("es-UY");
                var formatString = "dd/MM/yyyy HH:mm:ss"; //TODO: Pasar a constante

                if (value.ToString().Length == 10)
                    formatString = "dd/MM/yyyy";

                return Expression.Convert(Expression.Constant(DateTime.ParseExact(value, formatString, ci)), typeof(DateTime?));
            }

            return expression;
        }
    }
}
