using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Service
{
    public class PromotionService : Service<Promotion>, IPromotionService
    {
        /// <summary>
        /// This is PromotionService Constructer
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="unitOfWork"></param>
        public PromotionService(IRepository<Promotion> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
        }

        /// <summary>
        /// Get List Active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Promotion> GetActive()
        {
            return base.GetAll().Where(x => x.IsDelete != true);
        }

        /// <summary>
        /// Get list by condition
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public IEnumerable<Promotion> LoadByCondition(string select)
        {
            if (select == "active")
            {
                return base.GetAll().Where(x => x.IsDelete != true);
            }
            else
            {
                return base.GetAll().Where(x => x.IsDelete == true);
            }
        }

        /// <summary>
        /// This method is used to get a promotion by promotion id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Promotion GetById(int id)
        {
            var promotion = base.GetSingleById(id);
            
            if (promotion.Order.Any(o => o.PromotionID == id))
            {
                // Delete Order constraint
                promotion.Order = null;
                return promotion;
            }
            else
            {
                return promotion;
            }
        }

        /// <summary>
        /// Basic Search
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<Promotion> BasicSearch(string keyword)
        {
            if (keyword == "")
            {
                return GetActive();
            }
            else
            {
                return base.GetMulti(x => x.Name.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower()) && x.IsDelete != true);
            }
        }

        /// <summary>
        /// This method is used to Search advanced
        /// </summary>
        /// <param name="Name, startDate, endDate"></param>
        /// <returns></returns>
        public IEnumerable<Promotion> AdvancedSearch(string Name, string startDate, string endDate)
        {
            return base.GetAll().Where(x =>
                (Name == "" ? true : x.Name == Name)
                && (startDate == "" ? true : x.StartDate >= DateTime.Parse(startDate))
                && (endDate == "" ? true : x.EndDate <= DateTime.Parse(endDate))
                && x.IsDelete != true);
        }

        /// <summary>
        /// This method is used to get total page
        /// </summary>
        /// <param name="totalitem"></param>
        /// <param name="recordsPerPage"></param>
        /// <returns></returns>
        public int GetTotalPage(int totalitem, int recordsPerPage)
        {
            int nPages = totalitem / recordsPerPage;
            int m = totalitem % recordsPerPage;

            if (m > 0)
            {
                nPages++;
            }
            else{}

            return nPages;
        }

        /// <summary>
        /// This method is used to Paging
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Promotion> Paging(IEnumerable<Promotion> List, int recordsPerPage, int page)
        {
            var list = List
                .OrderBy(p => p.ID)
                .Skip((page - 1) * recordsPerPage)
                .Take(recordsPerPage)
                .ToList();

            return list;
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletePromotion(int id)
        {
            // Load to Update IsDelete = true
            var promotion = base.GetSingleById(id);

            // Nếu promorion is exist in any order => do not delete
            if (promotion.Order.Any(o => o.PromotionID == id))
            {
                return false;
            }
            else
            {
                promotion.IsDelete = true;

                base.Update(promotion);
                Save();

                return true;
            }
        }

        /// <summary>
        /// RecoveryPromotion 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RecoveryPromotion(int id)
        {
            try
            {
                var promotion = base.GetSingleById(id);
                promotion.IsDelete = false;

                base.Update(promotion);
                Save();

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
