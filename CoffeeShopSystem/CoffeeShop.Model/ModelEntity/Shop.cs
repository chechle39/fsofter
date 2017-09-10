using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Model.ModelEntity
{
    [Table("Shops")]
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID{ set; get; }

        [Required]
        [StringLength(255)]
        public string Name{ set; get; }

        public int? WardID{ set; get; }

        [StringLength(255)]
        public string DetailAddress{ set; get; }

        public string Description{ set; get; }

        public bool? IsDelete{ set; get; }

        public virtual ICollection<GroupTable> GroupTable{ set; get; }

        public virtual ICollection<Order> Order{ set; get; }

        public virtual ICollection<Product> Product{ set; get; }

        public virtual ICollection<Promotion> Promotion{ set; get; }

        [ForeignKey("WardID")]
        public virtual Ward Ward{ set; get; }

    //    public virtual IEnumerable<ShopUser> ShopUser{ set; get; }
    }
}
