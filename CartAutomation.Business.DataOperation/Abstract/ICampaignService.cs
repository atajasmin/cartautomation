using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Business.DataOperation.Abstract
{
    public interface ICampaignService
    {
        List<Campaign> GetAll();
        Campaign GetById(int id);
        List<Campaign> GetCurrentCampaignList(int categoryId,int totalQuantity);
        List<Campaign> GetByCategoryId(int id);
        int Add(Campaign campaign);
        void Delete(Campaign campaign);
        void Update(Campaign campaign);
    }
}
