using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Citys")]
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        public string Description { set; get; }

        public bool? IsDelete { set; get; }

        public virtual ICollection<District> District { set; get; }
    }
}
