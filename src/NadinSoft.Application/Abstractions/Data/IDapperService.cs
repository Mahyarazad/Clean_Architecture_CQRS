using System.Data;

namespace NadinSoft.Application.Abstractions.Data
{
    public interface IDapperService
    {
        IDbConnection CreateConnection();
    }
}
