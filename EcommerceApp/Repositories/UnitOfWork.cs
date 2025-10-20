using EcommerceApp.Context;
using EcommerceApp.Repositories.Interfaces;

namespace EcommerceApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategory _categoryRepository;
        private IProduct _productRepository;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProduct ProductRepository
        {
            get
            {
                return _productRepository = _productRepository ?? new ProductRepository(_context);
            }
        }

        public ICategory CategoryRepository
        {
            get 
            { 
                return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context); 
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
