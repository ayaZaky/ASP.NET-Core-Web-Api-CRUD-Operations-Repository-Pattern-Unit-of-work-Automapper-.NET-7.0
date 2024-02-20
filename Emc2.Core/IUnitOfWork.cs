using Emc2.Core.IRepositories;
using Emc2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Core
{
    public interface IUnitOfWork:IDisposable
    { 
        IGenericRepository<Service> Servieces { get; }
        IGenericRepository<Industry>Industries { get; }
        IGenericRepository<IndustryDescription> IndustryDescriptions { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Technology> Technologies { get; }
        IGenericRepository<TeamMember> TeamMembers { get; }
        IGenericRepository<Client> Clients { get; }
        IGenericRepository<ContactUs> Contacts { get; }
         
        int Complete();
    }
}
