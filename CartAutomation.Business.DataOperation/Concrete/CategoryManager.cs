using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.DataOperation.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public int Add(Category category)
        {
            _categoryDal.Add(category);
            return category.Id;
        }

        public void Delete(Category category)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            return _categoryDal.Get(filter: c => c.Id == id);
        }

        public Category GetByName(string name)
        {
            return _categoryDal.Get(filter: c => c.Name == name);
        }

        public void Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
