using NadinSoft.Domain.Abstractions;
using NadinSoft.Domain.Primitives;

namespace NadinSoft.Persistence.Repositories
{
    public  class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, Domain.Abstractions.ISpecification<T> specification)
        {
            IQueryable<T> query = inputQuery;

            foreach(WhereExpression<T> expression in specification.WhereExpressions)
            {
                query = query.Where(expression.Criteria);
            }


            return query;
        }
    }
}
