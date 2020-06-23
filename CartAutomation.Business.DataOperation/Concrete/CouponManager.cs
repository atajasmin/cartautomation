using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartAutomation.Business.DataOperation.Concrete
{
    public class CouponManager : ICouponService
    {
        private ICouponDal _couponDal;

        public CouponManager(ICouponDal couponDal)
        {
            _couponDal = couponDal;
        }
     
        public int Add(Coupon coupon)
        {
            _couponDal.Add(coupon);
            return coupon.Id;
        }

        public void Delete(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public List<Coupon> GetAll()
        {
            return _couponDal.GetList().ToList();
        }

        public List<Coupon> GetByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public Coupon GetById(int id)
        {
            return _couponDal.Get(filter: p => p.Id == id);
        }

        public List<Coupon> GetCurrentCouponList(decimal cartAmount)
        {
            return _couponDal.GetList(filter: p => p.MinAmountLimit <= cartAmount).ToList();//>=
        }

        public void Update(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
