using System;

namespace CoffeeShop.Model
{
    public class MaterialLogSearchViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ID { get; set; }
        public int MaterialID { get; set; }
        public int Inventory { get; set; }
        public int UnitPrice { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int IsDelete { get; set; }
        public int RowPerPage { get; set; }
    }
}
