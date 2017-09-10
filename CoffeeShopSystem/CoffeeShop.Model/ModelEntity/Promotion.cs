using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Promotions")]
    public class Promotion
    {
       
        [Key]
        public int ID { set; get; }

        public string Name { set; get; }

        public int? ShopID { set; get; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { set; get; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { set; get; }

        public decimal? BasePurchase { set; get; }

        public decimal? Discount { set; get; }

        public string Description { set; get; }

        public bool? IsDelete { set; get; }

        public virtual ICollection<Order> Order { set; get; }
        [ForeignKey("ShopID")]
        public virtual Shop Shop { set; get; }
    }
}
