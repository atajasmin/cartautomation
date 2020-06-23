using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.DataOperation.Abstract
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetAll();
        OrderDetail GetById(int id);
        List<OrderDetail> GetByOrderId(int id);
        List<OrderDetail> GetListByOrderIdCategoryId(int orderId,int categoryId);
        void Add(OrderDetail orderDetail);
        void Delete(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
    }
}
