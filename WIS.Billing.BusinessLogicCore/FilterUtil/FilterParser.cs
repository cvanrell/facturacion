using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.FilterUtil.Enums;
using WIS.BusinessLogic.FilterUtil.Expressions;
using WIS.BusinessLogic.FilterUtil.Tokens;

namespace WIS.BusinessLogic.FilterUtil
{
    public class FilterParser
    {
        private readonly FilterTokenizer _tokenizer;

        public FilterParser(FilterTokenizer tokenizer)
        {
            this._tokenizer = tokenizer;
        }
        public IFilterExpression Parse()
        {
            this._tokenizer.Tokenize();
            var filterList = this._tokenizer.GetPolishNotation().GetEnumerator();

            return this.ParseRecursive(ref filterList);
        }
        public IFilterExpression ParseRecursive(ref List<IFilterToken>.Enumerator tokenEnumerator)
        {
            if (tokenEnumerator.Current == null)
                tokenEnumerator.MoveNext();

            if (tokenEnumerator.Current is FilterTokenLiteral token)
            {
                tokenEnumerator.MoveNext();

                return new FilterExpressionLiteral(token.Value);
            }
            else
            {
                IFilterExpression operandLeft;
                IFilterExpression operandRight;

                switch (tokenEnumerator.Current.TokenType)
                {
                    case FilterTokenType.UNARY_NOT:
                        tokenEnumerator.MoveNext();
                        operandLeft = this.ParseRecursive(ref tokenEnumerator);
                        return new FilterExpressionNot(operandLeft);
                    case FilterTokenType.BINARY_AND:
                        tokenEnumerator.MoveNext();
                        operandLeft = this.ParseRecursive(ref tokenEnumerator);
                        operandRight = this.ParseRecursive(ref tokenEnumerator);
                        return new FilterExpressionAnd(operandLeft, operandRight);
                    case FilterTokenType.BINARY_OR:
                        tokenEnumerator.MoveNext();
                        operandLeft = this.ParseRecursive(ref tokenEnumerator);
                        operandRight = this.ParseRecursive(ref tokenEnumerator);
                        return new FilterExpressionOr(operandLeft, operandRight);
                    case FilterTokenType.BINARY_EQUAL:
                        tokenEnumerator.MoveNext();
                        operandLeft = this.ParseRecursive(ref tokenEnumerator);
                        operandRight = this.ParseRecursive(ref tokenEnumerator);
                        return new FilterExpressionEqual(operandLeft, operandRight);
                    case FilterTokenType.BINARY_GREATER_THAN:
                        tokenEnumerator.MoveNext();
                        operandLeft = this.ParseRecursive(ref tokenEnumerator);
                        operandRight = this.ParseRecursive(ref tokenEnumerator);
                        return new FilterExpressionGreaterThan(operandLeft, operandRight);
                    case FilterTokenType.BINARY_GREATER_THAN_OR_EQUAL:
                        tokenEnumerator.MoveNext();
                        operandLeft = this.ParseRecursive(ref tokenEnumerator);
                        operandRight = this.ParseRecursive(ref tokenEnumerator);
                        return new FilterExpressionGreaterThanOrEqual(operandLeft, operandRight);
                    case FilterTokenType.BINARY_LESS_THAN:
                        tokenEnumerator.MoveNext();
                        operandLeft = this.ParseRecursive(ref tokenEnumerator);
                        operandRight = this.ParseRecursive(ref tokenEnumerator);
                        return new FilterExpressionLessThan(operandLeft, operandRight);
                    case FilterTokenType.BINARY_LESS_THAN_OR_EQUAL:
                        tokenEnumerator.MoveNext();
                        operandLeft = this.ParseRecursive(ref tokenEnumerator);
                        operandRight = this.ParseRecursive(ref tokenEnumerator);
                        return new FilterExpressionLessThanOrEqual(operandLeft, operandRight);
                }
            }

            return null;
        }
    }
}
