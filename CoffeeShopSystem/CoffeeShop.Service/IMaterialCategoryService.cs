using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoffeeShop.Service
{
    public interface IMaterialCategoryService
    {
        MaterialCategory Add(MaterialCategory mate);

        void Update(MaterialCategory mate);

        void Delete(int id);
        // Get All
        IEnumerable<MaterialCategory> GetAll();

        MaterialCategory GetSingleByID(int id);

        IEnumerable<MaterialCategory> GetMulti(Expression<Func<MaterialCategory, bool>> predicate);

        IEnumerable<MaterialCategory> GetSearch(string keyword);

        IEnumerable<MaterialCategory> GetSearchPaging(string keyword, int index, int size, out int total);

        IEnumerable<MaterialCategory> GetSearchStatusPaging(string keyword, int status, int index, int size, out int total);

        void Save();
    }
}
