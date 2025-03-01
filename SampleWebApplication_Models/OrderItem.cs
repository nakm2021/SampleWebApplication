using System.ComponentModel.DataAnnotations;

namespace SampleWebApplication_Models
{
    public class OrderItem
    {
        [Key]
        public int OrderId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
