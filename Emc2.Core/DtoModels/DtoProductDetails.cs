using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Core.DtoModels
{
    public class DtoProductDetails
    {
        public int ProductId {  get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public List<string> Technologies { get; set; }
    }
}
