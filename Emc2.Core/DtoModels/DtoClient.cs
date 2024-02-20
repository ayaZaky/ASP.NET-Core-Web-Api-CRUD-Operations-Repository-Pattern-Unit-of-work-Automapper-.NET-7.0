using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Core.DtoModels
{
    public class DtoClient
    {
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
}
