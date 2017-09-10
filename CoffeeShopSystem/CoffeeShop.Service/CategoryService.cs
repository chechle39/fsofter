using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Service
{
    public interface ICategoryService
    {
        ProductCategory Add(ProductCategory cate);

        void Update(ProductCategory cate);

        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();

        ProductCategory GetByID(int id);
    }

    public class CategoryService : ICategoryService
    {
        private IProductCategoryRepository _cateRepository;
        private IUnitOfWork _unitOfWork;

        public CategoryService(IProductCategoryRepository cateRepository, IUnitOfWork unitOfWork)
        {
            this._cateRepository = cateRepository;
            this._unitOfWork = unitOfWork;
        }

        ProductCategory ICategoryService.Add(ProductCategory cate)
        {
           return _cateRepository.Add(cate);
            //throw new NotImplementedException();
        }

        void ICategoryService.Update(ProductCategory cate)
        {
            _cateRepository.Update(cate);
            //throw new NotImplementedException();
        }

        ProductCategory ICategoryService.Delete(int id)
        {
            return _cateRepository.Delete(id);
            //throw new NotImplementedException();
        }

        IEnumerable<ProductCategory> ICategoryService.GetAll()
        {
            return _cateRepository.GetAll();
            //throw new NotImplementedException();
        }

        ProductCategory ICategoryService.GetByID(int id)
        {
            return _cateRepository.GetSingleById(id);
            //throw new NotImplementedException();
        }
    }
}
