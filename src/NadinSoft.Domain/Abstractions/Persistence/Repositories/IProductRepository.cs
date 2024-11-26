using FluentResults;
using NadinSoft.Domain.Entities.Product;

namespace NadinSoft.Domain.Abstractions.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product value, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetProductListAsync(int pageNumber, int pageSize,
            string? nameFilter, string? manufactureEmailFilter, string? phoneFilter);
        Task UpdateAsync(Product value, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> DoesUserOwnThisProductAsync(Guid productId,Guid userId ,CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Guid productId ,CancellationToken cancellationToken = default);
    }
}
