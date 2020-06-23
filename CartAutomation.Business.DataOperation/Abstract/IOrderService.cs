using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.DataOperation.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(int id);
        List<Order> GetByUserId(int id);
        int Add(Order order);
        void Delete(Order order);
        void Update(Order order);
    }
}
