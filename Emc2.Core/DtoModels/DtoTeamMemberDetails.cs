using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Core.DtoModels
{
    public class DtoTeamMemberDetails
    {
        public int TeamMemberId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
    }
}
