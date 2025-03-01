namespace SampleWebApplication_DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        OrderRepository Order { get; }
        OrderItemRepository OrderItem { get; }
        int Save();
    }
}
