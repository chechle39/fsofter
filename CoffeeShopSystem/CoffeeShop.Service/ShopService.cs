using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using CoffeeShop.Data.Infrastructure;

namespace CoffeeShop.Service
{


    public class ShopService : IShopService
    {

        private IShopRepository shopRepository;
        private IUnitOfWork unitOfWork;
        


        /// <summary>
        /// Khởi tạo service
        /// </summary>
        /// <param name="shopRepository"></param>
        /// <param name="unitOfWork"></param>
        public ShopService(IShopRepository shopRepository, IUnitOfWork unitOfWork)
        {
            
            this.shopRepository = shopRepository;
            this.unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Validate shop model
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool ValidShopModel(Shop s)
        {   
            bool notValid = s.ID == null || s.Name == null || s.DetailAddress == null || s.WardID == null;
            if (notValid)
            {
                return false;
            }
            return true;
        }

        
        /// <summary>
        /// Create a new shop
        /// </summary>
        /// <param name="s">Model Shop</param>
        /// <returns>bool</returns>
        public bool CreateNew(Shop s)
        {   
            if (!ValidShopModel(s))
            {
                return false;
            }


            if (s.IsDelete == null)
            {
                s.IsDelete = false;
            }

            try
            {
                shopRepository.Add(s);
                this.unitOfWork.Commit();
            }

            catch 
            { 
                return false; 
            }

            return true;
           


        }

        /// <summary>
        /// Get Both Managing and Deleted Shop 
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<Shop> GetAll()
        {
            return shopRepository.GetAll();
        }

        public IEnumerable<Shop> GetAllNonDelete()
        {
            return shopRepository.GetMulti( s=> s.IsDelete == false,null);
        }


        /// <summary>
        /// Only get deleted shop
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Shop> GetAllDeleted()
        {
            return shopRepository.GetMulti(s => s.IsDelete == true, null);
        }

        /// <summary>
        /// Update info for a shop
        /// </summary>
        /// <param name="s">Model Shop</param>
        /// <returns>bool</returns>
        public bool Update(Shop s)
        {
            if (!ValidShopModel(s))
            {
                return false;
            }

            try
            {
                Shop originShop = shopRepository.GetsingleByCondition(shop => shop.ID == s.ID, null);
                originShop.Name = s.Name;
                originShop.Description = s.Description;
                if (s.IsDelete == null)
                {
                    originShop.IsDelete = false;
                }

                else
                {
                    originShop.IsDelete = true;
                }
                originShop.DetailAddress = s.DetailAddress;
                originShop.WardID = s.WardID;
                shopRepository.Update(originShop);
                this.unitOfWork.Commit();
            }
            catch 
            { 
                return false; 
            }

            return true;
        }

        /// <summary>
        /// Soft Delete A Shop
        /// </summary>
        /// <param name="s">Model Shop</param>
        /// <returns>bool</returns>
        public bool SoftDelete(Shop s)
        {
            if (!ValidShopModel(s))
            {
                return false;
            }

            s.IsDelete = true;
            try
            {
                shopRepository.Update(s);
                this.unitOfWork.Commit();
            }

            catch 
            {
                return false;
            }


            return true;
        }


        /// <summary>
        /// Recover A Shop
        /// </summary>
        /// <param name="s">Model Shop</param>
        /// <returns>bool</returns>
        public bool Recover(Shop s)
        {
            if (!ValidShopModel(s))
            {
                return false;
            }

            s.IsDelete = false;
            try
            {
                shopRepository.Update(s);
                this.unitOfWork.Commit();
            }

            catch
            {
                return false;
            }


            return true;
        }




        /// <summary>
        /// Get shop by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Shop GetShopByID(int id)
        {  
             
            return shopRepository.GetSingleById(id);
        }

        /// <summary>
        /// Search by name or description
        /// </summary>
        /// <param name="Filter">Search keyword</param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"> 'ID' , 'Name'</param>
        /// <param name="type">'asc' , 'desc'</param>
        /// <param name="option">'Managing' , 'Deleted'</param>
        /// <returns></returns>
        public IEnumerable<Shop> SearchByNameOrDescription(string Filter, int curPage = 1,int pageSize =5, string sort="ID", string type="asc", string option ="Managing")
        {
            int total;

            if (Filter == "") // Empty filter
            {
                if (option == "Managing")
                {
                    return shopRepository.GetMultiPagingForShop(s => s.IsDelete == false, out total, curPage, pageSize, sort, type);
                }

                // option = deleted 
                return shopRepository.GetMultiPagingForShop(s => s.IsDelete == true, out total, curPage, pageSize, sort, type);
            }

            // Has input Filter
            else
            {
                if (option == "Managing")
                {
                    return shopRepository.GetMultiPagingForShop(u => (u.Name.Contains(Filter) || u.Description.Contains(Filter)) && u.IsDelete == false, out total, curPage, pageSize, sort, type);
                }

                //option = deleted
                return shopRepository.GetMultiPagingForShop(u => (u.Name.Contains(Filter) || u.Description.Contains(Filter)) && u.IsDelete == true, out total, curPage, pageSize, sort, type);
                
            }
        }


        /// <summary>
        /// Change Sort Type of the column being sort
        /// </summary>
        /// <param name="type">"asc" , "desc"</param>
        /// <param name="condition"> if pass in sort == "ID" , only change sort type for column ID , other column will remain their default sort type </param>
        /// <returns></returns>
        public string ChangeSortType(string type, bool condition)
        {

            if (!condition)
            {
                return "default"; //default sort type
            }

            if (type == "asc")
            {
                type = "desc";
            }
            else
            {
                type = "asc";
            }
            return type;


        }



        /// <summary>
        /// Get All Managing Shop , support pagination and sort
        /// </summary>
        /// <param name="sort">ID , Name</param>
        /// <param name="type">asc ,desc</param>
        /// <param name="curpage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Shop> GetAllNonDeleteWithPaginationAndSort(string sort,string type, int curpage, int pageSize)
        {
            int total; // total result found for that page
            IEnumerable<Shop> listShop;

            listShop = shopRepository.GetMultiPagingForShop(s => s.IsDelete == false,out total,curpage , pageSize , sort, type);
            return listShop.ToList();
        }



        /// <summary>
        /// Get All Deleted Shop , support pagination and sort
        /// </summary>
        /// <param name="sort">ID , Name</param>
        /// <param name="type">asc ,desc</param>
        /// <param name="curpage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Shop> GetAllDeletedWithPaginationAndSort(string sort, string type, int curpage, int pageSize)
        {
            int total; // total result found for that page
            IEnumerable<Shop> listShop;
            listShop = shopRepository.GetMultiPagingForShop(s => s.IsDelete == true, out total, curpage, pageSize, sort, type);
            return listShop.ToList();
        }

        public bool IsUniqueName(string input, int shopID)
        {
            bool check = true;

            Shop model;
            if (shopID == 0)
            {
                model = shopRepository.GetAll()
                    .Where(x => x.Name == input && x.IsDelete == false).FirstOrDefault();
            }

            else
            {
                model = shopRepository.GetAll()
                    .Where(x => x.Name == input && x.ID != shopID && x.IsDelete == false).FirstOrDefault();
            }

            if (model != null)
            {
                check = false;
            }



            return check;
        }

    }
}