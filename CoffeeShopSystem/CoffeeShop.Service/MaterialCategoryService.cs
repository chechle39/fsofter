using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoffeeShop.Service
{
    public class MaterialCategoryService : IMaterialCategoryService
    {
        private IMaterialCategoryRepository materialCategoryRepository;
        private IUnitOfWork unitOfWork;
        public MaterialCategoryService(IMaterialCategoryRepository repoMateCate, IUnitOfWork unit) 
        {
            this.materialCategoryRepository = repoMateCate;
            this.unitOfWork = unit;
        }
        public IEnumerable<MaterialCategory> GetAll()
        {
            return materialCategoryRepository.GetAll(); //select duoc group Promotion tuong ung.
        }
        public MaterialCategory Add(MaterialCategory mateCate)
        {
            var rs = materialCategoryRepository.Add(mateCate);
            return rs;
        }

        public void Delete(int id)
        {
            var mateCate = GetSingleByID(id);
            mateCate.IsDelete = !mateCate.IsDelete;
            materialCategoryRepository.Update(mateCate);
        }

        public MaterialCategory GetSingleByID(int id)
        {
            return materialCategoryRepository.GetSingleById(id);
        }

        public void Update(MaterialCategory mateCate)
        {
            materialCategoryRepository.Update(mateCate);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<MaterialCategory> GetSearch(string keyword)
        {
            return materialCategoryRepository.GetMulti(x => x.Name.Contains(keyword) && (!x.IsDelete ?? true));
        }

        public IEnumerable<MaterialCategory> GetSearchPaging(string keyword, int index, int size, out int total)
        {
            return materialCategoryRepository.GetMultiPaging(x => x.Name.Contains(keyword), out total, index, size);
        }

        public IEnumerable<MaterialCategory> GetMulti(Expression<Func<MaterialCategory, bool>> predicate)
        {
            return materialCategoryRepository.GetMulti(predicate);
        }

        public IEnumerable<MaterialCategory> GetSearchStatusPaging(string keyword, int status, int index, int size, out int total)
        {

            Expression<Func<MaterialCategory, bool>> func;
            switch (status)
            {
                case 2:
                    func = x => x.IsDelete == false && (x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.CreatedDate.ToString().Contains(keyword));
                    break;
                case 3:
                    func = x => x.IsDelete == true && (x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.CreatedDate.ToString().Contains(keyword));
                    break;
                default:
                    func = x => (x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.CreatedDate.ToString().Contains(keyword));
                    break;
            }
            var query = materialCategoryRepository.GetMulti(func);
            total = query.Count();
            query = query.OrderByDescending(x => x.CreatedDate)
              .Skip((index - 1) * size)
              .Take(size).AsEnumerable();

            return query;
        }
    }
}