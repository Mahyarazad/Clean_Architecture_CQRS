using FluentResults;
using NadinSoft.Application.Features.Products;

namespace NadinSoft.Presentation.Helpers
{
    public static class ResultErrorParser
    {
        public static object ParseResultError(Result<ProductDTO> result)
        {
            return new
            {
                Errors = result.Reasons.Select(x => x.Message).ToList()
            };
        }
    }
}
