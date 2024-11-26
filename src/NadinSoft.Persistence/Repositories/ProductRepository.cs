using Dapper;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Application.Abstractions.Data;
using NadinSoft.Domain.Abstractions.Persistence.Repositories;
using NadinSoft.Domain.Entities.Product;
using NadinSoft.Persistence.Data;

namespace NadinSoft.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext context, IDapperService dapper) : IProductRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IDapperService _dapper = dapper;

        public async Task AddAsync(Product value, CancellationToken cancellationToken = default)
        {
            await _context.Set<Product>().AddAsync(value, cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Product>().FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Product>().Where(p => p.Id == productId).ExecuteDeleteAsync();
        }

        public Task UpdateAsync(Product value, CancellationToken cancellationToken = default)
        {
            _context.Set<Product>().Update(value);
           return Task.CompletedTask;
        }

        public Task<bool> DoesUserOwnThisProductAsync(Guid productId, Guid userId, CancellationToken cancellationToken = default)
        {
            return _context.Set<Product>().AnyAsync(p => p.Id == productId && p.UserId == userId, cancellationToken);
        }

        public Task<bool> AnyAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return _context.Set<Product>().AnyAsync(x => x.Id == productId);
        }


        public async Task<IEnumerable<Product>> GetProductListAsync(
            int pageNumber, int pageSize,
            string? nameFilter, string? manufactureEmailFilter, string? phoneFilter)
        {

            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber, System.Data.DbType.Int16, System.Data.ParameterDirection.Input);
            parameters.Add("PageSize", pageSize, System.Data.DbType.Int16, System.Data.ParameterDirection.Input);
            parameters.Add("NameFilter", nameFilter, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("ManufactureEmailFilter", manufactureEmailFilter, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("PhoneFilter", phoneFilter, System.Data.DbType.String, System.Data.ParameterDirection.Input);


            using var connection = _dapper.CreateConnection();
            return await  connection.QueryAsync<Product>("GetFilteredDataWithPagination", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
