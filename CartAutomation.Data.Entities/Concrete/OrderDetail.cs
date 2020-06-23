using CartAutomation.Data.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.Entities.Concrete
{
    public class OrderDetail : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscountAmount { get; set; }
    }
}
