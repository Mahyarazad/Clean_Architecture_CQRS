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

        public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Product>().FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

        public Task SoftDeleteAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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

        public async Task<List<Product>> GetProductListAsync(string? nameFilter, string? manufactureEmailFilter, string? phoneFilter, CancellationToken cancellationToken)
        {
            // We can refactor this block of code and use specification pattern
            var query = _context.Set<Product>().AsQueryable();

            if(!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query.Where(p => p.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            if(!string.IsNullOrWhiteSpace(manufactureEmailFilter))
            {
                query = query.Where(p => p.ManufactureEmail.ToLower().Contains(manufactureEmailFilter.ToLower()));
            }

            if(!string.IsNullOrWhiteSpace(phoneFilter))
            {
                query = query.Where(p => p.ManufactureEmail.ToLower().Contains(phoneFilter.ToLower()));
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
