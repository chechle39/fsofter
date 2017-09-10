using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Model.ModelEntity
{
    public class ApplicationUserGroup
    {

        [Key]
        [Column(Order = 1)]
        public int UserId { set; get; }

        [Key]
        [Column(Order = 2)]
        public int GroupId { set; get; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { set; get; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroup { set; get; }
    }
}
