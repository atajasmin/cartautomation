using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.DataOperation.Abstract
{
    public interface ICouponService
    {
        List<Coupon> GetAll();
        Coupon GetById(int id);
        List<Coupon> GetByCategoryId(int id);
        int Add(Coupon coupon);
        void Delete(Coupon coupon);
        void Update(Coupon coupon);

        List<Coupon> GetCurrentCouponList(decimal cartAmount);
    }
}
