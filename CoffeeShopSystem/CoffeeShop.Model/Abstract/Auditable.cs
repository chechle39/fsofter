using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Model.Abstract
{
    public class Auditable : IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDelete { get; set; }
    }
}
