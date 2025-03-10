﻿using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// テスト用にオーバーライド
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            OrderItem orderItem = (OrderItem)obj;
            return OrderId == orderItem.OrderId && ProductId == orderItem.ProductId && Quantity == orderItem.Quantity && Price == orderItem.Price;
        }

        /// <summary>
        /// テスト用にオーバーライド
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, ProductId, Quantity, Price);
        }
    }
}
