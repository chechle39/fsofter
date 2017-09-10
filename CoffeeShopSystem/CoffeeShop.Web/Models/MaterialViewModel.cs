using System;

namespace CoffeeShop.Web.Models
{
    public class MaterialViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Nullable<int> CategoryID { get; set; }

        public Nullable<decimal> UnitPrice { get; set; }

        public Nullable<int> Inventory { get; set; }

        public string Description { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        public Nullable<bool> IsDelete { get; set; }

        public string CategoryName { get; set; }
    }
}