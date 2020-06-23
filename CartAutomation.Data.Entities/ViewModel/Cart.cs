using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.Entities.ViewModel
{
    public class Cart
    {
        public List<CartItem> cartItemList { get; set; }
        public int UserId { get; set; }
    }
}
