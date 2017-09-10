using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System;

namespace CoffeeShop.Service
{
    

    public class ProductCategoryService : Service<ProductCategory>, IProductCategoryService
    {
        private IProductCategoryRepository _cateRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IRepository<ProductCategory> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
        }

        public void DeleteCate(int id)
        {
            var cate = base.GetSingleById(id);
            cate.IsDelete = true;
            base.Update(cate);
            //throw new NotImplementedException();
        }

        public void Restore(int id)
        {
            var category = base.GetSingleById(id);
            category.IsDelete = false;
            base.Update(category);
            //throw new NotImplementedException();
        }
    }
}
