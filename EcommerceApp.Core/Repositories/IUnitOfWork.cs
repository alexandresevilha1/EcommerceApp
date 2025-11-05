namespace EcommerceApp.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }

        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        Task<int> CommitAsync();
    }
}
