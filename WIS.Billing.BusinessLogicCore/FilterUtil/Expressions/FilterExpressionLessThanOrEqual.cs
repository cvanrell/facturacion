﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.FilterUtil.Expressions
{
    public class FilterExpressionLessThanOrEqual : IFilterExpression
    {
        private IFilterExpression ExpressionLeft { get; set; }
        private IFilterExpression ExpressionRight { get; set; }

        public FilterExpressionLessThanOrEqual(IFilterExpression expressionLeft, IFilterExpression expressionRight)
        {
            this.ExpressionLeft = expressionLeft;
            this.ExpressionRight = expressionRight;
        }

        public Expression Evaluate(FilterExpressionContext context)
        {
            return ExpressionExtension.LessThanOrEqual(this.ExpressionLeft.Evaluate(context), this.ExpressionRight.Evaluate(context));
        }
    }
}
