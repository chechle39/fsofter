using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Wards")]
    public class Ward
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int DistrictID { set; get; }

        [StringLength(255)]
        public string Name { set; get; }

        public string Description { set; get; }

        public bool? IsDelete { set; get; }

        [ForeignKey("DistrictID")]
        public virtual District District { set; get; }

        public virtual ICollection<Shop> Shop { set; get; }

       
        public virtual ICollection<ApplicationUser> ApplicationUser { set; get; }
    }
}
