using CartAutomation.Data.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.Entities.Concrete
{
    public enum DiscountType { AMOUNT = 1, RATE = 2};
    public class Campaign : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int MinNumberOfProduct { get; set; }
        public int Rate { get; set; }
        public decimal DiscountAmount { get; set; }
        public int DiscountType { get; set; }

        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public bool Active { get; set; }

    }
}
