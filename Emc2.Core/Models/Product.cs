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
    public class Product
    {

        [Key]
        public int ProductId { get; set; } 
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<Technology> ProductTechnologies { get; set; }
     public Product() {
            ProductTechnologies = new List<Technology>();
        }
    }

}
