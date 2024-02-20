using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Core.Models
{
    public class Industry
    {
            [Key]
            public int IndustryId { get; set; } 
            public string Name { get; set; }
            public byte[] Icon { get; set; } 
            public virtual ICollection<IndustryDescription> IndustryDescriptions { get; set; }
        

    }
}
