using SampleWebApplication_DataAccess.Data;
using SampleWebApplication_Models;

namespace SampleWebApplication_DataAccess.Repository
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        public OrderItemRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
