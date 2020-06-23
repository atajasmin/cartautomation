using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.Entities.ViewModel
{
    public class OrderDetailSummary
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}
