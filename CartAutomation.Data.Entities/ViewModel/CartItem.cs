using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.Entities.ViewModel
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
