using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartAutomation.Business.DataOperation.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public int Add(Product product)
        {
            _productDal.Add(product);
            return product.Id;
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public List<Product> GetAll()
        {
           return  _productDal.GetList().ToList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(filter:p => p.Id == id);
        }

        public List<Product> GetByCategoryId(int id)
        {
            return _productDal.GetList(filter: p => (int)p.CategoryId == id).ToList();
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public Product GetByName(string name)
        {
            return _productDal.Get(filter: p => p.Name == name);
        }
    }
}
