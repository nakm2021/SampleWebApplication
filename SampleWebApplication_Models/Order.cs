using System.ComponentModel.DataAnnotations;

namespace SampleWebApplication_Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
