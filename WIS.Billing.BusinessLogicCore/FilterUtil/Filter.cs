using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.FilterUtil;
using WIS.CommonCore.FilterComponents;

namespace WIS.BusinessLogicCore.FilterUtil
{
    public class Filter : IFilter
    {        
        public IQueryable<T> ApplyFilter<T>(IQueryable<T> query, string filterString)
        {
            var lambda = this.InterpretQueryString<T>(filterString);

            var result = query;

            if (lambda != null)
                result = result.Where(lambda);

            return result;
        }
        public IQueryable<T> ApplyFilter<T>(IQueryable<T> query, List<FilterCommand> commands)
        {
            var filterString = this.ParseCommandList(commands);

            return this.ApplyFilter(query, filterString);
        }

        private Expression<Func<T, bool>> InterpretQueryString<T>(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                return null;

            var tokenizer = new FilterTokenizer(queryString);
            var parser = new FilterParser(tokenizer);
            var context = new FilterExpressionContext();

            context.RegisterType(typeof(T), "type");

            IFilterExpression filter = parser.Parse();

            Expression exp = filter.Evaluate(context);

            return Expression.Lambda<Func<T, bool>>(exp, context.ExpressionParameters.ToArray());
        }
        private string ParseCommandList(List<FilterCommand> filters)
        {
            List<string> filterString = this.ParseIndividualFilterQuery(filters);

            return string.Join(" AND ", filterString);
        }
        private List<string> ParseIndividualFilterQuery(List<FilterCommand> filters)
        {
            StringBuilder sb = new StringBuilder();

            var filterStringList = new List<string>();

            foreach (var filter in filters)
            {
                bool hasNegation = false;
                var value = filter.Value.Trim();
                var prefix = value.Substring(0, 1);

                if (prefix == "!")
                {
                    sb.Append("!(");
                    hasNegation = true;
                    value = value.TrimStart('!');
                    prefix = value.Substring(0, 1);
                }

                sb.Append(":").Append(filter.ColumnId);

                if (prefix != "=" && prefix != ">" && prefix != "<" && prefix != ">=" && prefix != "<=")
                    sb.Append("=");

                sb.Append(value);

                if (hasNegation)
                    sb.Append(")");

                filterStringList.Add(sb.ToString());

                sb.Clear();
            }

            return filterStringList;
        }
    }
}
