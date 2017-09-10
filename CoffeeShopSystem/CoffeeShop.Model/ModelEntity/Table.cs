using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Tables")]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        public int GroupTableID { set; get; }

        [Column(TypeName = "ntext")]
        public string Description { set; get; }

        public bool? IsDelete { set; get; }

        [ForeignKey("GroupTableID")]
        public virtual GroupTable GroupTable { set; get; }

        public virtual ICollection<Order> Order { set; get; }
    }
}
