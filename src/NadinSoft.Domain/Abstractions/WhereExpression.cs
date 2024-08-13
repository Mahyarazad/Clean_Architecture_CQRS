using NadinSoft.Domain.Primitives;
using System.Linq.Expressions;

namespace NadinSoft.Domain.Abstractions
{
    public class WhereExpression<T>
    {
        public Expression<Func<T, bool>> Criteria { get;set; }
    }
}