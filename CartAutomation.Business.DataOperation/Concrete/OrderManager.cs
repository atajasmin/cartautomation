using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.DataOperation.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        
        public int Add(Order order)
        {
            _orderDal.Add(order);
            return order.Id;
        }

        public void Delete(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            return _orderDal.Get(filter: o => o.Id == id);
        }

        public List<Order> GetByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            _orderDal.Update(order);
        }
    }
}
