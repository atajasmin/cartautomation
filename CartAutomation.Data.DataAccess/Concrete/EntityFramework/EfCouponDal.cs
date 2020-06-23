using CartAutomation.Data.Base.DataAccess.EntityFramework;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.DataAccess.Concrete.EntityFramework
{
    public class EfCouponDal : EfEntityRepositoryBase<Coupon, CartAutomationContext>, ICouponDal
    {
    }
}
