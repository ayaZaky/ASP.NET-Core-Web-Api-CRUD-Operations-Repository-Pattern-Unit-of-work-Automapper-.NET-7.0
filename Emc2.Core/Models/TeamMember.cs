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
    public class TeamMember
    {
        [Key]
        public int TeamMemberId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; } 
        public byte[] Image { get; set; }

         

    }
}
