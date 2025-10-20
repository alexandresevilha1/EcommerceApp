namespace EcommerceApp.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IProduct ProductRepository { get; }
        ICategory CategoryRepository { get; }

        void Commit();

    }
}
