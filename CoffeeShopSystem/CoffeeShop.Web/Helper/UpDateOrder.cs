using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Model.ModelEntity
{
    public static class CurrentContext
    {
        /// <summary>
        /// Đưa hóa đơn cần cập nhật vào session
        /// </summary>
        /// <returns></returns>
        public static UpdateOrder SetAllItem()
        {
            var ret = (UpdateOrder)HttpContext.Current.Session["updateOrder"];
            if (ret == null)
            {
                ret = new UpdateOrder();
                HttpContext.Current.Session["updateOrder"] = ret;
            }
            return ret;
        }


        /// <summary>
        /// Lấy hóa đơn từ session
        /// </summary>
        /// <returns>UpdateOrder</returns>
        public static UpdateOrder GetAll()
        {
            var ret = (UpdateOrder)HttpContext.Current.Session["updateOrder"];
            return ret;
        }
    }

    public class UpdateOrder
    {
        public List<OrderProductItem> Items { get; set; } //Danh sách các item

        public List<OrderProductItem> ItemsDelete { get; set; } //Danh sách các item bị xóa

        public int ID { get; set; }
        public int tableID { get; set; }
        public bool? status { get; set; }

        /// <summary>
        /// Đưa thông tin hóa đơn vào session
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableID"></param>
        /// <param name="status"></param>
        public void AddInfoOrder(int id, int tableID, bool? status)
        {
            if (this.Items.Count > 0)
            {
                this.Items.Clear();
            }
            if (this.ItemsDelete.Count > 0)
            {
                this.ItemsDelete.Clear();
            }
            this.ID = id;
            this.tableID = tableID;
            this.status = status;
        }


        /// <summary>
        /// Cập nhật hóa đơn trong session
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="status"></param>
        public void UpdateInfoOrder(int tableID, bool? status)
        {
            this.tableID = tableID;
            this.status = status;
        }


        /// <summary>
        /// Khởi tạo 2 list
        /// </summary>
        public UpdateOrder()
        {
            this.Items = new List<OrderProductItem>();
            this.ItemsDelete = new List<OrderProductItem>();
        }

        /// <summary>
        /// Thêm 1 item vào session
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(OrderProductItem item)
        {
            this.Items.Add(item);
        }

        /// <summary>
        /// Cập nhật lại item trong sesion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="q"></param>
        public void UpdateItem(int id, int q)
        {
            var item = this.Items.Where(i => i.ID == id).FirstOrDefault();
            if (item != null)
            {
                item.quantity = q;
            }
        }

        /// <summary>
        /// Xóa 1 item khỏi session
        /// </summary>
        /// <param name="id"></param>
        public void RemoveItem(int id)
        {
            var item = this.Items.Where(i => i.ID == id).FirstOrDefault();
            if (item != null)
            {
                this.ItemsDelete.Add(item);
                this.Items.Remove(item);
            }
        }

        /// <summary>
        /// Tính tổng tiền khi có thay đổi
        /// </summary>
        /// <returns></returns>
        public decimal TotalMoney()
        {
            decimal ret = 0;
            foreach (var i in this.Items)
            {
                ret += decimal.Parse((i.price * i.quantity).ToString());
            }
            return ret;
        }
    }



    public class OrderProductItem
    {
        public int ID { get; set; }
        public Nullable<int> quantity { get; set; }
        public decimal price { get; set; }

    }
}
