using CartAutomation.Data.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.Entities.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int State { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal TotalCampaignAmount { get; set; }
        public decimal TotalCouponAmount { get; set; }
    }
}
