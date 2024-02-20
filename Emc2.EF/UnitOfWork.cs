using Emc2.Core;
using Emc2.Core.IRepositories;
using Emc2.Core.Models;
using Emc2.EF.DBContext;
using Emc2.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _dbcontext;

        public IGenericRepository<Service> Servieces { get; private set; }

        public IGenericRepository<Industry> Industries { get; private set; }

        public IGenericRepository<IndustryDescription> IndustryDescriptions { get; private set; }

        public IGenericRepository<Product> Products { get; private set; }

        public IGenericRepository<Technology> Technologies { get; private set; }

        public IGenericRepository<TeamMember> TeamMembers { get; private set; } 

        public IGenericRepository<Client> Clients { get; private set; } 

        public IGenericRepository<ContactUs> Contacts { get; private set; }

         

        public UnitOfWork(AppDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            Servieces = new GenericRepository<Service>(_dbcontext);
            Industries = new GenericRepository<Industry>(_dbcontext);
            IndustryDescriptions= new GenericRepository<IndustryDescription>(_dbcontext);
            Products = new GenericRepository<Product>(_dbcontext);
            Technologies= new GenericRepository<Technology>(_dbcontext);
            TeamMembers=new GenericRepository<TeamMember>(_dbcontext);
            Clients = new GenericRepository<Client>(_dbcontext);
            Contacts=new GenericRepository<ContactUs>(_dbcontext);


        }

        public int Complete()
        {
           return  _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
