using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoffeeShop.Model.ModelEntity;

namespace CoffeeShop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Update(T entity);

        T Delete(T entity);

        T Delete(int id);

        T GetSingleById(int id);

        T GetSingleById(int? id);

        IEnumerable<Shop> GetMultiPagingForShop(Expression<Func<Shop, bool>> filter, out int total, int curPage = 0, int pageSize = 50, string sort = "", string type = "");
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        T GetsingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAll(string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);

        void DeleteMulti(Expression<Func<T, bool>> where);
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);
    }
}