using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.Entities.ViewModel
{
    public class OrderSummary
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal DeliveryAmount { get; set; }
    }
}
