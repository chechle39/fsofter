using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("MaterialLogs")]
    public class MaterialLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int? MaterialID { set; get; }

        public int? EmployeeID { set; get; }

        public int? Inventory { set; get; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { set; get; }

        public bool? Type { set; get; }

        public string Description { set; get; }

        public DateTime? CreatedDate { set; get; }

        public bool? IsDelete { set; get; }
        [ForeignKey("MaterialID")]
        public virtual Material Material { set; get; }
      
      
        [ForeignKey("EmployeeID")]
        public virtual ApplicationUser ApplicationUser { set; get; }
    }
}
