using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface IPromotionService : IService<Promotion>
    {
        // Get List Active
        IEnumerable<Promotion> GetActive();

        // Load List by condition
        IEnumerable<Promotion> LoadByCondition(string select);

        // Basic Search
        IEnumerable<Promotion> BasicSearch(string keyword);

        //Advanced Search
        IEnumerable<Promotion> AdvancedSearch(string Name, string startDate, string endDate);

        // Get GetTotalPage
        int GetTotalPage(int totalitem, int recordsPerPage);

        // Paging
        IEnumerable<Promotion> Paging(IEnumerable<Promotion> list, int recordsPerPage, int page);

        // Delete
        bool DeletePromotion(int id);

        //Recovery
        bool RecoveryPromotion(int id);

        //GetById
        Promotion GetById(int id);
    }
}
