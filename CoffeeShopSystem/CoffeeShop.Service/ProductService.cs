using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System;

namespace CoffeeShop.Service
{
    
    public class ProductService : Service<Product>, IProductService
    {
        IProductRepository _productRepository;
        IUnitOfWork _unitOfWork;
        public ProductService(IRepository<Product> productService, IUnitOfWork unitOfWork) : base(productService, unitOfWork)
        {
            
        }
        //xu ly delete for product
        public void DeleteProduct(int id)
        {
            var product = base.GetSingleById(id);
            product.IsDelete = true;
            base.Update(product);
           // throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllPaging(int page, int size, out int totalRow)
        {
            return _productRepository.GetMultiPaging(x => (!x.IsDelete ?? true), out totalRow, page, size);
        }

        public void RestoreProduct(int id)
        {
            var product = base.GetSingleById(id);
            product.IsDelete = false;
            base.Update(product);
            //throw new NotImplementedException();
        }
    }
}
