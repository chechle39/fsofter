using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Orders")]
    public class Order
    {
     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ set; get; }

        public int TableID{ set; get; }

        public int? PromotionID{ set; get; }

       

        public DateTime? SetDate{ set; get; }

        [Column(TypeName = "money")]
        public decimal? TotalMoney{ set; get; }

        public bool? Status{ set; get; }

        public bool? isDelete{ set; get; }

        public int ShopID{ set; get; }

        [ForeignKey("PromotionID")]
        public virtual Promotion Promotion{ set; get; }

        [ForeignKey("TableID")]
        public virtual Table Table{ set; get; }

   

        public virtual ICollection<OrderProduct> OrderProduct{ set; get; }
        [ForeignKey("ShopID")]
        public virtual Shop Shop{ set; get; }

       
        public int CustomerId { set; get; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Users { set; get; }
    }
}
