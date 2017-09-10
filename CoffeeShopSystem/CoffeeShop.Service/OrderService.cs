using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CoffeeShop.Service
{
    public class OrderService : IOrderService
    {
        IOrderRepository orderRepository;
        IUnitOfWork unitOfWork;
        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            this.orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// update order
        /// </summary>
        /// <param name="order"></param>
        public void Update(Order order)
        {
            var orderUpdate = orderRepository.GetSingleById(order.ID);
            orderUpdate.TableID = order.TableID;
            orderUpdate.Status = order.Status;
            decimal total = 0;
            foreach (var p in orderUpdate.OrderProduct.Where(p => p.isDelete == false).ToList())
            {
                total = total + (decimal)p.Money;
            }
            orderUpdate.TotalMoney = total;
            orderRepository.Update(orderUpdate);
        }

        /// <summary>
        /// Delete order,update IsDelate=true
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var orderUpdate = orderRepository.GetSingleById(id);
            orderUpdate.isDelete = true;
            orderRepository.Delete(id);
        }


        /// <summary>
        /// Get all by ShopID
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetAll()
        {
            return orderRepository.GetAll().ToList();
        }


        /// <summary>
        /// get order by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetByID(int id)
        {
            return orderRepository.GetSingleById(id);
        }

        /// <summary>
        /// Save database
        /// </summary>
        public void Save()
        {
            unitOfWork.Commit();
        }

        /// <summary>
        /// Search by ID or Table
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="lst"></param>
        /// <returns>dynamic</returns>
        public dynamic SearchByIdOrTable(string keyword, List<Order> lst)
        {
            var list = lst.ToList()
                          .Where(o => o.ID.ToString().ToLower().Contains(keyword.ToLower())
                                      || o.Table.Name.ToString().ToLower().Contains(keyword.ToLower())
                                      || o.Users.FullName.ToLower().Contains(keyword.ToLower()))
                          .Select(o => new
                          {
                              id = o.ID,
                              tableName = o.Table.Name,
                              userName = o.Users.FullName,
                              promoName = o.Promotion.Name,
                              setDate = o.SetDate,
                              totalMoney = o.TotalMoney,
                              status = o.Status,
                              tableID = o.TableID
                          })
                          .ToList();
            return list;
        }


        /// <summary>
        ///Filter by Date and TableID
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="TableID"></param>
        /// <returns>dynamic</returns>
        public dynamic SearchAdvanced(string customerName, string fromDate, string toDate, int TableID, ref List<Order> lst)
        {
            var list = lst.Where(o =>
                                ((customerName == null || customerName == "") ? true : o.Users.FullName.ToLower().Contains(customerName.ToLower()))
                                && ((fromDate == "" || fromDate == null) ? true : o.SetDate.Value >= DateTime.Parse(fromDate))
                                && ((toDate == "" || fromDate == null) ? true : o.SetDate.Value <= DateTime.Parse(toDate))
                                && (TableID == 0 ? true : o.TableID == TableID))
                          .Select(o => new
                          {
                              id = o.ID,
                              tableName = o.Table.Name,
                              userName = o.Users.FullName,
                              promoName = o.Promotion.Name,
                              setDate = o.SetDate,
                              totalMoney = o.TotalMoney,
                              status = o.Status,
                              tableID = o.TableID
                          })
                          .ToList();
            return list;
        }

        /// <summary>
        /// Filter by Status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="lst"></param>
        /// <returns>dynamic</returns>
        public dynamic GetByStatus(string status, ref List<Order> lst)
        {
            var list2 = orderRepository.GetAll().ToList();
            var list = orderRepository.GetAll().ToList()
                                               .Select(o => new
                                               {
                                                   id = o.ID,
                                                   tableName = o.Table.Name,
                                                   userName = o.Users.FullName,
                                                   promoName = o.Promotion.Name,
                                                   setDate = o.SetDate,
                                                   totalMoney = o.TotalMoney,
                                                   status = o.Status,
                                                   tableID = o.TableID
                                               })
                                               .ToList();

            if (status == "1")
            {
                lst = list2;
                return list;

            }
            else if (status == "2")
            {
                lst = list2.Where(o => o.Status == false).ToList();
                return list.Where(o => o.status == false);

            }
            else
            {
                lst = list2.Where(o => o.Status == true).ToList();
                return list.Where(o => o.status == true);
            }
        }
    }
}
