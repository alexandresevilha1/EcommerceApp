using EcommerceApp.Core.Repositories;
using EcommerceApp.Infrastructure.Context;
using EcommerceApp.Infrastructure.Repositories;


namespace EcommerceApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICartItemRepository CartItemRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            CartRepository = new CartRepository(_context);
            CartItemRepository = new CartItemRepository(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
