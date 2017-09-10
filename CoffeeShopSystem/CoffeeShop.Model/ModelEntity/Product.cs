using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int ProductCategoryID { set; get; }

        public int? ShopID { set; get; }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { set; get; }

        [Column(TypeName = "ntext")]
        public string Desciption { set; get; }

        public DateTime? StatDate { set; get; }

        public DateTime? EndDate { set; get; }

        public int? Discount { set; get; }

        public bool? IsDelete { set; get; }

        public virtual ICollection<OrderProduct> OrderProduct { set; get; }
        [ForeignKey("ProductCategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }
        [ForeignKey("ShopID")]
        public virtual Shop Shop { set; get; }
    }
}
