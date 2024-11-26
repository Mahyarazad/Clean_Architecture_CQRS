using System.Net;

namespace NadinSoft.Domain.Abstractions
{
    public abstract class BaseResponse()
    {
        protected HttpStatusCode StatusCode { get; init; }
    }
}
