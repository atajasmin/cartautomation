using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartAutomation.Business.DataOperation.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        private IOrderDetailDal _orderDetailDal;
        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public void Add(OrderDetail orderDetail)
        {
            _orderDetailDal.Add(orderDetail);
        }

        public void Delete(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetByOrderId(int id)
        {
            return _orderDetailDal.GetList(filter: d => d.OrderId == id).ToList();
        }

        public List<OrderDetail> GetListByOrderIdCategoryId(int orderId, int categoryId)
        {
            return _orderDetailDal.GetList(filter: d => d.OrderId == orderId && d.CategoryId == categoryId).ToList();
        }

        public void Update(OrderDetail orderDetail)
        {
            _orderDetailDal.Update(orderDetail);
        }
    }
}
