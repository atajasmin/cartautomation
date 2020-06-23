using CartAutomation.Data.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
