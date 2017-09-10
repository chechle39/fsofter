using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("GroupTables")]
    public class GroupTable
    {
         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        [Column(TypeName = "ntext")]
        public string Description { set; get; }

        [Column(TypeName = "money")]
        public decimal? Surcharge { set; get; }

        public bool? IsDelete { set; get; }

        public int? ShopID { set; get; }

        public virtual ICollection<Table> Table { set; get; }

        [ForeignKey("ShopID")]
        public virtual Shop Shop { set; get; }
    }
}
