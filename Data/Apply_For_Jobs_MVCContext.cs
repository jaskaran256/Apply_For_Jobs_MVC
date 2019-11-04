using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Apply_For_Jobs_Core_Webapp.BusinessLayer;

namespace Apply_For_Jobs_MVC.Models
{
    //Connects the databse and map the model objects using entity framework.
    public class Apply_For_Jobs_MVCContext : DbContext
    {
        public Apply_For_Jobs_MVCContext (DbContextOptions<Apply_For_Jobs_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Apply_For_Jobs_Core_Webapp.BusinessLayer.Advertisement> Advertisement { get; set; }

        public DbSet<Apply_For_Jobs_Core_Webapp.BusinessLayer.Application> Application { get; set; }

        public DbSet<Apply_For_Jobs_Core_Webapp.BusinessLayer.Candidate> Candidate { get; set; }

        public DbSet<Apply_For_Jobs_Core_Webapp.BusinessLayer.Employer> Employer { get; set; }
    }
}
