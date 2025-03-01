using SampleWebApplication_DataAccess.Data;
using SampleWebApplication_Models;

namespace SampleWebApplication_DataAccess.Repository
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
