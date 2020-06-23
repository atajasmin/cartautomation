using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.DataOperation.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        Product GetByName(string name);
        List<Product> GetByCategoryId(int id);
        int Add(Product product);
        void Delete(Product product);
        void Update(Product product);
    }
}
