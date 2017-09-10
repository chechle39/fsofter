using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Web.Models
{
    public class WardViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DistrictID { get; set; }
        public string NameDistrict { get; set; }
        public string NameCity { get; set; }
        public bool? IsDelete { get; set; }
    }
}