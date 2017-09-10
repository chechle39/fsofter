using CoffeeShop.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoffeeShop.Service
{
    public abstract class Service<T> : IService<T> where T : class
    {
        IRepository<T> repository;
        IUnitOfWork unitOfWork;

        public Service(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public virtual T Add(T group)
        {
            return repository.Add(group);
        }

        public virtual T Delete(int id)
        {
            return repository.Delete(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }

        public virtual T GetByID(int id)
        {
            return repository.GetSingleById(id);
        }

        public virtual void Update(T group)
        {
            repository.Update(group);
        }

        public virtual bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return repository.CheckContains(predicate);
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return repository.Count(where);
        }

        public virtual IEnumerable<T> GetAll(string[] includes = null)
        {
            return repository.GetAll(includes);
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            return repository.GetMulti(predicate, includes);
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            return repository.GetMultiPaging(filter, out total, index, size, includes);
        }

        public virtual T GetsingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            return repository.GetsingleByCondition(expression, includes);
        }

        public virtual T GetSingleById(int id)
        {
            return repository.GetSingleById(id);
        }

        public virtual T Delete(T entity)
        {
            return repository.Delete(entity);
        }

        public virtual void Save()
        {
            unitOfWork.Commit();
        }
    }
}
