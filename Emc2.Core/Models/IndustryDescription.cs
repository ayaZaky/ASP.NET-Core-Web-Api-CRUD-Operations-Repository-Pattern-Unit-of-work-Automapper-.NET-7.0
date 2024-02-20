using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emc2.Core.Models  
{
    public class IndustryDescription
    {
        [Key]
        public int DescriptionId { get; set; } 
        public string DescriptionLine { get; set; }

        [ForeignKey("IndustryId")]
        public int IndustryId { get; set; }
        public virtual Industry Industry { get; set; }
    }
}