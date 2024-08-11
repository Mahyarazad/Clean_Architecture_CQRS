using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Abstractions.Persistence.Repositories;
using NadinSoft.Domain.Entities.Product;
using NadinSoft.Persistence.Data;

namespace NadinSoft.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product value, CancellationToken cancellationToken = default)
        {
            await _context.Set<Product>().AddAsync(value, cancellationToken);
        }

        public Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Product>().FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

        public Task SoftDeleteAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Product value, CancellationToken cancellationToken = default)
        {
            _context.Set<Product>().Update(value);
        }
    }
}
