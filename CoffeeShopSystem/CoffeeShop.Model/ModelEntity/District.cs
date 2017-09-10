using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Districts")]
    public class District
    {
        [Key]
        public int ID { set; get; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        public int CityID { set; get; }

        public string Description { set; get; }

        public bool? IsDelete { set; get; }

        [ForeignKey("CityID")]
        public virtual City City { set; get; }

        public virtual ICollection<Ward> Ward { set; get; }
    }
}
