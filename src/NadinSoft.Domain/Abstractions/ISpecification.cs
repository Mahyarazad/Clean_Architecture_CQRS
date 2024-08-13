using NadinSoft.Domain.Primitives;

namespace NadinSoft.Domain.Abstractions
{
    public interface ISpecification<T> where T : BaseEntity
    {
        List<WhereExpression<T>> WhereExpressions { get; }
        List<string> IncludeStrings { get; }

        // Include and paganitation comes here

    }

}
