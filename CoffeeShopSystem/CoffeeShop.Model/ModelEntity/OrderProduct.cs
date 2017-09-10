using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("OrderProducts")]
    public class OrderProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int OrderID { set; get; }

        public int ProductID { set; get; }

        public int Quantity { set; get; }

        [Column(TypeName = "money")]
        public decimal? Price { set; get; }

        [Column(TypeName = "money")]
        public decimal? Money { set; get; }

        public bool? isDelete { set; get; }

        public virtual Order Order { set; get; }

        public virtual Product Product { set; get; }
    }
}
