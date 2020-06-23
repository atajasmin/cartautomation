using CartAutomation.Business.Core.DependencyResolves.Ninject;
using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartAutomation.Business.CartOperation
{
    public abstract class DiscountCalculatorBase
    {
        public IOrderService OrderService = InstanceFactory.GetInstance<IOrderService>();
        public IOrderDetailService OrderDetailService = InstanceFactory.GetInstance<IOrderDetailService>();
        public abstract DiscountCalculateItem CalculateAndApplyDiscount(Order order);

        public abstract void CalculateAndApplyUnitDiscount(DiscountCalculateItem discountCalculateItem);
    }

    public class CampaignDiscountCalculator : DiscountCalculatorBase
    {
        private ICampaignService _campaignService = InstanceFactory.GetInstance<ICampaignService>();

        public override DiscountCalculateItem CalculateAndApplyDiscount(Order order)
        {
            //step1: sepette her kategoride toplam kaç ürün var
            List<OrderDetail> orderDetailList = OrderDetailService.GetByOrderId(order.Id);
            var orderDetailSummaryList = orderDetailList.GroupBy(p => p.CategoryId).Select(g => new
            {
                CategoryId = g.Key,
                Quantity = g.Sum(t => t.Quantity)
            });

            //step2: her kategori için geçerli kampanyaları bul ve indirimi hesapla
            List<DiscountCalculateItem> discountCalculateItemList = new List<DiscountCalculateItem>();
            foreach (var item in orderDetailSummaryList)
            {
                List<Campaign> currentCampaignList = _campaignService.GetCurrentCampaignList(item.CategoryId,item.Quantity);
                if (currentCampaignList.Count != 0)
                {
                    foreach (var campaignItem in currentCampaignList)
                    {
                        DiscountCalculateItem discountCalculateItem = new DiscountCalculateItem();
                        discountCalculateItem.OrderId = order.Id;
                        discountCalculateItem.CategoryId = item.CategoryId;
                        discountCalculateItem.TotalQuantity = item.Quantity;
                        if (campaignItem.DiscountType == (int)DiscountType.RATE)
                        {
                            discountCalculateItem.DiscountAmount = (order.OriginalPrice * campaignItem.Rate) / 100;
                        }
                        else if (campaignItem.DiscountType == (int)DiscountType.AMOUNT)
                        {
                            discountCalculateItem.DiscountAmount = campaignItem.DiscountAmount;
                        }
                        discountCalculateItemList.Add(discountCalculateItem);
                    }
                }
                else
                {
                    return null;
                }
            }

            //step3:Max indirim tutarlı olanı bul 
            var maxDiscountCampaign = discountCalculateItemList.OrderByDescending(i => i.DiscountAmount).First();

            //indirimi order'a uygula
            order.TotalPrice = order.TotalPrice - maxDiscountCampaign.DiscountAmount;
            order.TotalCampaignAmount = maxDiscountCampaign.DiscountAmount;
            OrderService.Update(order);

            return maxDiscountCampaign;
        }

        public override void CalculateAndApplyUnitDiscount(DiscountCalculateItem discountCalculateItem)
        {
            decimal unitDiscountAmount = discountCalculateItem.DiscountAmount / discountCalculateItem.TotalQuantity;
            List<OrderDetail> orderDetailList = OrderDetailService.GetListByOrderIdCategoryId(discountCalculateItem.OrderId,discountCalculateItem.CategoryId);
            foreach(var orderDetail in orderDetailList)
            {
                orderDetail.TotalDiscountAmount = orderDetail.TotalDiscountAmount + unitDiscountAmount * orderDetail.Quantity;
                OrderDetailService.Update(orderDetail);
            }
        }

    }

    public class CouponDiscountCalculator : DiscountCalculatorBase
    {
        private ICouponService _couponService = InstanceFactory.GetInstance<ICouponService>();
        
        public override DiscountCalculateItem CalculateAndApplyDiscount(Order order)
        {
            //step1: sepette toplam kaç ürün var
            List<OrderDetail> orderDetailList = OrderDetailService.GetByOrderId(order.Id);
            var orderDetailSummaryList = orderDetailList.GroupBy(p => p.OrderId).Select(g => new
            {
                OrderId = g.Key,
                Quantity = g.Sum(t => t.Quantity)
            });

            //step2:sepet için geçerli kuponları bul ve her kupan için indirimlari hesapla
            List<DiscountCalculateItem> discountCalculateItemList = new List<DiscountCalculateItem>();
            List<Coupon> currentCouponList = _couponService.GetCurrentCouponList(order.TotalPrice); 
            if (currentCouponList.Count != 0)
            {
                foreach (var couponItem in currentCouponList)
                {
                    DiscountCalculateItem discountCalculateItem = new DiscountCalculateItem();
                    discountCalculateItem.OrderId = orderDetailSummaryList.First().OrderId;
                    discountCalculateItem.TotalQuantity = orderDetailSummaryList.First().Quantity;
                    if (couponItem.DiscountType == (int)DiscountType.RATE)
                    {
                        discountCalculateItem.DiscountAmount = (order.TotalPrice * couponItem.Rate) / 100;
                    }
                    else if (couponItem.DiscountType == (int)DiscountType.AMOUNT)
                    {
                        discountCalculateItem.DiscountAmount = couponItem.DiscountAmount;
                    }
                    discountCalculateItemList.Add(discountCalculateItem);
                }
            }
            else
            {
                return null;
            }

            //step3:Max indirim tutarlı olanı bul 
            var maxDiscountCampaign = discountCalculateItemList.OrderByDescending(i => i.DiscountAmount).First();

            //indirimi order'a uygula
            order.TotalPrice = order.TotalPrice - maxDiscountCampaign.DiscountAmount;
            order.TotalCouponAmount = maxDiscountCampaign.DiscountAmount;
            OrderService.Update(order);

            return maxDiscountCampaign;
        }

        public override void CalculateAndApplyUnitDiscount(DiscountCalculateItem discountCalculateItem)
        {
            decimal unitDiscountAmount = discountCalculateItem.DiscountAmount / discountCalculateItem.TotalQuantity;
            List<OrderDetail> orderDetailList = OrderDetailService.GetByOrderId(discountCalculateItem.OrderId);
            foreach (var orderDetail in orderDetailList)
            {
                orderDetail.TotalDiscountAmount = orderDetail.TotalDiscountAmount + unitDiscountAmount * orderDetail.Quantity;
                OrderDetailService.Update(orderDetail);
            }
        }
    }

    public class DiscountCalculateItem
    {
        public int CategoryId { get; set; }
        public int TotalQuantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public int OrderId { get; set; }
    }

}
