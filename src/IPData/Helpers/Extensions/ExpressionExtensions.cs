using System;
using System.Linq.Expressions;
using IPData.Models;

namespace IPData.Helpers.Extensions
{
    internal static class ExpressionExtensions
    {
        public static string PropertyName(this Expression<Func<IPInfo, object>> expression)
        {
            switch (expression.Body)
            {
                case MemberExpression body:
                    return body.Member.Name;
                case UnaryExpression body:
                    var operand = body.Operand as MemberExpression;
                    return operand.Member.Name;
                default:
                    throw new InvalidOperationException("Invalid expression");
            }
        }
    }
}
