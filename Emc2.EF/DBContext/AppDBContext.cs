using Emc2.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.EF.DBContext
{
    public  class AppDBContext:DbContext
    {
        public AppDBContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(); // Ensure this line is present
                                                    // Other configurations...
        }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Service> services { get; set; }
        public DbSet<Industry> industries { get; set; }
        public DbSet<IndustryDescription> industryDescriptions { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Technology> technologies { get; set; } 
        public DbSet<TeamMember> teamMembers { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<ContactUs> contacts { get; set; }
    }
     

}
