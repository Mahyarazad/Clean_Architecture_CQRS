using NadinSoft.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NadinSoft.Domain.Abstractions.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product value, CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        void UpdateAsync(Product value, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
