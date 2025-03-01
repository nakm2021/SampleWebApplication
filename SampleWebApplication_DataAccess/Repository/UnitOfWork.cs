using SampleWebApplication_DataAccess.Data;
using SampleWebApplication_DataAccess.Repository.IRepository;

namespace SampleWebApplication_DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public OrderRepository Order { get; private set; }
        public OrderItemRepository OrderItem { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Order = new OrderRepository(_db);
            OrderItem = new OrderItemRepository(_db);
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
