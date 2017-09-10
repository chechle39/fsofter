using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Materials")]
    public class Material
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        public int? CategoryID { set; get; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { set; get; }

        public int? Inventory { set; get; }

        public string Description { set; get; }

        public DateTime? CreatedDate { set; get; }

        public bool? IsDelete { set; get; }

        [ForeignKey("CategoryID")]
        public virtual MaterialCategory MaterialCategory { set; get; }

        public virtual ICollection<MaterialLog> MaterialLog { set; get; }
    }
}
