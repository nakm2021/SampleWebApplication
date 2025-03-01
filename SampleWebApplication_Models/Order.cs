using System.ComponentModel.DataAnnotations;

namespace SampleWebApplication_Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Order order = (Order)obj;
            return OrderId == order.OrderId && CustomerId == order.CustomerId && OrderDate == order.OrderDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, CustomerId, OrderDate);
        }
    }
}
