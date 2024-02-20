using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Core.DtoModels
{
    public class DtoTeamMember
    { 
        public string Name { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
    }
}
