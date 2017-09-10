using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("MaterialCategorys")]
    public class MaterialCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        public string Description { set; get; }

        public DateTime? CreatedDate { set; get; }

        public bool? IsDelete { set; get; }

        public virtual ICollection<Material> Material { set; get; }
    }
}
