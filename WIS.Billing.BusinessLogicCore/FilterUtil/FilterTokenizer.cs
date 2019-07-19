using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.FilterUtil.Tokens;

namespace WIS.BusinessLogicCore.FilterUtil
{
    public class FilterTokenizer
    {
        private string _string;
        public List<IFilterToken> Tokens { get; set; }
        private readonly Dictionary<Enums.FilterTokenType, int> OperatorPrecedence = new Dictionary<Enums.FilterTokenType, int>
        {
            { Enums.FilterTokenType.PARENTHESIS_OPEN, 1 },
            { Enums.FilterTokenType.BINARY_OR, 2 },
            { Enums.FilterTokenType.BINARY_AND, 3 },
            { Enums.FilterTokenType.BINARY_EQUAL, 4 },
            { Enums.FilterTokenType.BINARY_GREATER_THAN, 4 },
            { Enums.FilterTokenType.BINARY_GREATER_THAN_OR_EQUAL, 4 },
            { Enums.FilterTokenType.BINARY_LESS_THAN, 4 },
            { Enums.FilterTokenType.BINARY_LESS_THAN_OR_EQUAL, 4 },
            { Enums.FilterTokenType.UNARY_NOT, 5 },
        };

        public FilterTokenizer(string text)
        {
            this._string = text;
            this.Tokens = new List<IFilterToken>();
        }

        public void Tokenize()
        {
            using (var reader = new StringReader(_string))
            {
                while (reader.Peek() != -1)
                {
                    while (char.IsWhiteSpace((char)reader.Peek()))
                    {
                        reader.Read();
                    }

                    if (reader.Peek() == -1)
                        break;

                    var c = (char)reader.Peek();

                    switch (c)
                    {
                        case '!':
                            this.Tokens.Add(new FilterTokenNot());
                            reader.Read();
                            break;
                        case '(':
                            this.Tokens.Add(new FilterTokenParenthesisOpen());
                            reader.Read();
                            break;
                        case ')':
                            this.Tokens.Add(new FilterTokenParenthesisClose());
                            reader.Read();
                            break;
                        case '<':
                            reader.Read();
                            if (reader.Peek() == '=')
                            {
                                this.Tokens.Add(new FilterTokenLessThanOrEqual());
                                reader.Read();
                            }
                            else
                            {
                                this.Tokens.Add(new FilterTokenLessThan());
                            }
                            break;
                        case '>':
                            reader.Read();
                            if (reader.Peek() == '=')
                            {
                                this.Tokens.Add(new FilterTokenGreaterThanOrEqual());
                                reader.Read();
                            }
                            else
                            {
                                this.Tokens.Add(new FilterTokenGreaterThan());
                            }
                            break;
                        case '=':
                            this.Tokens.Add(new FilterTokenEqual());
                            reader.Read();
                            break;
                        default:
                            this.Tokens.Add(this.ParseKeyword(reader));
                            break;
                    }
                }
            }
        }
        public IFilterToken ParseKeyword(StringReader reader)
        {
            var text = new StringBuilder();

            while (this.IsValidContentChar((char)reader.Peek()))
            {
                text.Append((char)reader.Read());
            }

            var potentialKeyword = text.ToString().ToLower();

            switch (potentialKeyword)
            {
                case "and":
                    return new FilterTokenAnd();
                case "or":
                    return new FilterTokenOr();
                case "between":
                    return new FilterTokenBetween();
                default:
                    return new FilterTokenLiteral(potentialKeyword);
            }
        }
        public List<IFilterToken> GetPolishNotation()
        {
            Queue<IFilterToken> outputQueue = new Queue<IFilterToken>();
            Stack<IFilterToken> stack = new Stack<IFilterToken>();

            int index = 0;

            var tokens = this.GetPivotedList();

            tokens.Reverse();

            while (index < tokens.Count)
            {
                IFilterToken token = tokens[index];
                IFilterToken topToken;

                switch (token.TokenType)
                {
                    case Enums.FilterTokenType.LITERAL:
                        outputQueue.Enqueue(token);
                        break;
                    case Enums.FilterTokenType.PARENTHESIS_OPEN:
                        stack.Push(token);
                        break;
                    case Enums.FilterTokenType.PARENTHESIS_CLOSE:
                        topToken = stack.Pop();
                        while (topToken.TokenType != Enums.FilterTokenType.PARENTHESIS_OPEN)
                        {
                            outputQueue.Enqueue(topToken);
                            topToken = stack.Pop();
                        }
                        break;
                    default:
                        while (stack.Count > 0 && this.OperatorPrecedence[stack.Peek().TokenType] >= this.OperatorPrecedence[token.TokenType])
                        {
                            outputQueue.Enqueue(stack.Pop());
                        }

                        stack.Push(token);
                        break;
                }

                index++;
            }

            while (stack.Count > 0)
            {
                outputQueue.Enqueue(stack.Pop());
            }

            return outputQueue.Reverse().ToList();
        }
        private List<IFilterToken> GetPivotedList()
        {
            var pivotedList = new List<IFilterToken>();

            foreach (var token in this.Tokens)
            {
                if (token.TokenType == Enums.FilterTokenType.PARENTHESIS_OPEN)
                    pivotedList.Add(new FilterTokenParenthesisClose());
                else if (token.TokenType == Enums.FilterTokenType.PARENTHESIS_CLOSE)
                    pivotedList.Add(new FilterTokenParenthesisOpen());
                else
                    pivotedList.Add(token);
            }

            return pivotedList;
        }

        private bool IsValidContentChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '/' || c == ':' || c == ';'
                || c == '\'' || c == '.' || c == ',' || c == '_' || c == '-'
                || c == '%';
        }
    }
}
