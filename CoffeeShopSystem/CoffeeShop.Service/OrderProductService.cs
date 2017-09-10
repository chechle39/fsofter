using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Service
{
    public class OrderProductService : IOrderProductService
    {
        IOrderProductRepository _orderRepository;
        IUnitOfWork _unitOfWork;
        public OrderProductService(IOrderProductRepository tableRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = tableRepository;
            this._unitOfWork = unitOfWork;
        }
        public void Add(OrderProduct order)
        {
            _orderRepository.Add(order);
        }
        public void Update(OrderProduct table)
        {
           var op= _orderRepository.GetSingleById(table.ID);
            op.Quantity = table.Quantity;
            op.Money = op.Quantity * op.Price;
            _orderRepository.Update(op);
        }
        public void Delete(int id)
        {
            var orderUpdate = _orderRepository.GetSingleById(id);
            orderUpdate.isDelete = true;
            _orderRepository.Update(orderUpdate);
        }

        public IEnumerable<OrderProduct> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public OrderProduct GetByID(int id)
        {
            return _orderRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public List<OrderProduct> GetListOrderProductByOrderID(int id)
        {
            return _orderRepository.GetListOrderProductByOrderID(id).Where(od=>od.isDelete==false).ToList();
        }
    }
}
