using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Core.DtoModels
{
    public class DtoIndustryDetails
    {
        public int IndustryId { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }
        public List<string> DescriptionLines { get; set; }
    }
}
