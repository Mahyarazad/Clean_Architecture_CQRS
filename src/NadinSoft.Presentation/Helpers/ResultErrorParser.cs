using FluentResults;
using NadinSoft.Domain.Entities.Product;

namespace NadinSoft.Presentation.Helpers
{
    public static class ResultErrorParser
    {
        public static object ParseResultError(Result<Product> result)
        {
            return new
            {
                Errors = result.Reasons.Select(x => x.Message).ToList()
            };
        }
    }
}
