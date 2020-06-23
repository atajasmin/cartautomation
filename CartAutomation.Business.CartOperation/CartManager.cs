using CartAutomation.Business.Core.DependencyResolves.Ninject;
using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.Entities.Concrete;
using CartAutomation.Data.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.CartOperation
{
    public class CartManager
    {
        private IOrderService _orderService = InstanceFactory.GetInstance<IOrderService>();
        private IOrderDetailService _orderDetailService = InstanceFactory.GetInstance<IOrderDetailService>();
        private IProductService _productService = InstanceFactory.GetInstance<IProductService>();
        private ICategoryService _categoryService = InstanceFactory.GetInstance<ICategoryService>();

        public DiscountCalculatorBase DiscountCalculator { get; set; }

        public DeliveryCostCalculatorBase DeliveryCostCalculator { get; set; }

        public Order SaveCart(Cart cart) 
        {
            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.State = 0;
            order.UserId = cart.UserId;
            int orderId = _orderService.Add(order);

            decimal cartAmount = 0;
            foreach (var cartItem in cart.cartItemList)
            {
                cartAmount = cartAmount + cartItem.Product.Price * cartItem.Quantity;
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderId = orderId;
                orderDetail.CategoryId = cartItem.Product.CategoryId;
                orderDetail.ProductId = cartItem.Product.Id;
                orderDetail.Quantity = cartItem.Quantity;
                orderDetail.UnitPrice = cartItem.Product.Price;
                orderDetail.TotalPrice = cartItem.Product.Price * cartItem.Quantity;
                _orderDetailService.Add(orderDetail);
            }

            order.OriginalPrice = cartAmount;
            order.TotalPrice = cartAmount;
            order.State = 1;
            _orderService.Update(order);

            return order;
        }
     
        public void ApplyDiscount(Order order)
        {
            //indirimi bul uygula
            DiscountCalculateItem discountCalculateItem =  DiscountCalculator.CalculateAndApplyDiscount(order);
            //indirmi orderDetail'a paylaştır
            if(discountCalculateItem != null)
            {
                DiscountCalculator.CalculateAndApplyUnitDiscount(discountCalculateItem);
            }
        }

        public void ApplyDeliveryCost(Order order)
        {
            //kargo ücretini bul
            decimal costDelivery = DeliveryCostCalculator.DeliveryCostCalculate(order);
            //kargo ücretini sepet tutarına ekle
            order.TotalPrice = order.TotalPrice + costDelivery;
            order.DeliveryCost = costDelivery;
            _orderService.Update(order);
        }

        public void SetOrderForPayment(Order order)
        {
            //tahsilata gitmeden önce bazı iş kuralları buraya yazılabilir
            _orderService.Update(order);
        }

        public OrderSummary GetOrderSummary(Order order)
        {
            OrderSummary orderSummary = new OrderSummary();
            orderSummary.OrderId = order.Id;
            orderSummary.TotalAmount = order.TotalPrice;
            orderSummary.DeliveryAmount = order.DeliveryCost;
            return orderSummary;
        }

        public List<OrderDetailSummary> GetOrderDetailSummary(Order order)
        {
            List<OrderDetailSummary> orderDetailSummaryList = new List<OrderDetailSummary>();
            List <OrderDetail> orderDetailList = _orderDetailService.GetByOrderId(order.Id);
            foreach (var orderDetail in orderDetailList)
            {
                OrderDetailSummary orderDetailSummary = new OrderDetailSummary();
                orderDetailSummary.OrderDetailId = orderDetail.Id;
                orderDetailSummary.OrderId = orderDetail.OrderId;
                orderDetailSummary.CategoryName = _categoryService.GetById(orderDetail.CategoryId).Name;
                orderDetailSummary.ProductName = _productService.GetById(orderDetail.ProductId).Name;
                orderDetailSummary.Quantity = orderDetail.Quantity;
                orderDetailSummary.UnitPrice = orderDetail.UnitPrice;
                orderDetailSummary.TotalPrice = orderDetail.TotalPrice;
                orderDetailSummary.TotalDiscount = orderDetail.TotalDiscountAmount;

                orderDetailSummaryList.Add(orderDetailSummary);
            }

            return orderDetailSummaryList;
        }
    }
}
