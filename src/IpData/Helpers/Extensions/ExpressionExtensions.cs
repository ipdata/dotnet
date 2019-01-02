using System;
using System.Linq.Expressions;
using IpData.Models;

namespace IpData.Helpers.Extensions
{
    internal static class ExpressionExtensions
    {
        public static string PropertyName(this Expression<Func<IpInfo, object>> expression)
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
