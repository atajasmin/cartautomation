using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Business.DataOperation.Concrete;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.DataAccess.Concrete.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.Core.DependencyResolves.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();
            Bind<ICategoryDal>().To<EfCategoryDal>().InSingletonScope();

            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<EfProductDal>().InSingletonScope();

            Bind<IOrderService>().To<OrderManager>().InSingletonScope();
            Bind<IOrderDal>().To<EfOrderDal>().InSingletonScope();

            Bind<IOrderDetailService>().To<OrderDetailManager>().InSingletonScope();
            Bind<IOrderDetailDal>().To<EfOrderDetailDal>().InSingletonScope();

            Bind<ICampaignService>().To<CampaignManager>().InSingletonScope();
            Bind<lCampaignDal>().To<EfCampaignDal>().InSingletonScope();

            Bind<ICouponService>().To<CouponManager>().InSingletonScope();
            Bind<ICouponDal>().To<EfCouponDal>().InSingletonScope();

        }
    }
}
