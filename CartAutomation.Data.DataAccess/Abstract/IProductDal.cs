using CartAutomation.Data.Base.DataAccess;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
    }
}
