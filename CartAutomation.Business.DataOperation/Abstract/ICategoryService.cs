using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.DataOperation.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        Category GetByName(string name);
        int Add(Category category);
        void Delete(Category category);
        void Update(Category category);
    }
}
