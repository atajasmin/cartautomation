using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartAutomation.Business.DataOperation.Concrete
{
    public class CampaignManager : ICampaignService
    {
        private lCampaignDal _campaignDal;

        public CampaignManager(lCampaignDal campaignDal)
        {
            _campaignDal = campaignDal;
        }
      
        public int Add(Campaign campaign)
        {
            _campaignDal.Add(campaign);
            return campaign.Id;
        }

        public void Delete(Campaign campaign)
        {
            throw new NotImplementedException();
        }

        public List<Campaign> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Campaign> GetByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public Campaign GetById(int id)
        {
            return _campaignDal.Get(filter: c => c.Id == id);
        }

        public List<Campaign> GetCurrentCampaignList(int categoryId, int totalQuantity)
        {
            return _campaignDal.GetList(filter: c => c.CategoryId == categoryId && c.MinNumberOfProduct <= totalQuantity).ToList();
        }

        public void Update(Campaign campaign)
        {
            throw new NotImplementedException();
        }
    }
}
