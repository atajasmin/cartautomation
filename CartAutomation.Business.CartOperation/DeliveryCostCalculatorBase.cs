using CartAutomation.Business.Core.DependencyResolves.Ninject;
using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.Entities.Concrete;
using CartAutomation.Data.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartAutomation.Business.CartOperation
{
    public abstract class DeliveryCostCalculatorBase
    {
        public abstract decimal DeliveryCostCalculate(Order order);

        public decimal CostPerDelivery;

        public decimal CostPerProduct;

        public decimal FixedCost;
        public DeliveryCostCalculatorBase(decimal costPerDelivery, decimal costPerProduct, decimal fixedCost)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }

    }

    public class StandardDeliveryCostCalculator : DeliveryCostCalculatorBase
    {
        public IOrderDetailService _orderDetailService = InstanceFactory.GetInstance<IOrderDetailService>();

        public StandardDeliveryCostCalculator(decimal costPerDelivery, decimal costPerProduct, decimal fixedCost) :base(costPerDelivery,costPerProduct, fixedCost)
        {
          
        }
        public override decimal DeliveryCostCalculate(Order order)
        {
            List<OrderDetail> orderDetailList = _orderDetailService.GetByOrderId(order.Id);
            var distinctCategoryId = orderDetailList.Select(d => d.CategoryId).Distinct();
            var distinctProductId = orderDetailList.Select(d => d.ProductId).Distinct();
            decimal costDelivery = (CostPerDelivery * distinctCategoryId.Count()) + (CostPerProduct * distinctProductId.Count()) + FixedCost;
            return costDelivery;
        }
    }

    //EfsaneCuma'da farklı hesaplama olgoritması kullanılabilir
    public class BlackFridayDeliveryCostCalculator : DeliveryCostCalculatorBase
    {
        public BlackFridayDeliveryCostCalculator(decimal costPerDelivery, decimal costPerProduct, decimal fixedCost) : base(costPerDelivery, costPerProduct, fixedCost)
        {

        }
        
        public override decimal DeliveryCostCalculate(Order order)
        {
            throw new NotImplementedException();
        }
    }
 }
