using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emc2.Core.Models
{
    public class Technology
    {
        [Key]
        public int TechnologyId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; } 

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
         
    }
}