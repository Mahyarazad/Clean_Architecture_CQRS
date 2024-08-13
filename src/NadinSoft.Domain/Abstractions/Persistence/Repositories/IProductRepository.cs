using NadinSoft.Domain.Entities.Product;

namespace NadinSoft.Domain.Abstractions.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product value, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<List<Product>> GetProductListAsync(string? nameFilter, string? manufactureEmailFilter, string? phoneFilter, CancellationToken cancellationToken = default);
        Task UpdateAsync(Product value, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> DoesUserOwnThisProductAsync(Guid productId,Guid userId ,CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Guid productId ,CancellationToken cancellationToken = default);
    }
}
