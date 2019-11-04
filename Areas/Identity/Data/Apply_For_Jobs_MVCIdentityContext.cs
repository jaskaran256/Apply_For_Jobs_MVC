using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Apply_For_Jobs_MVC.Models
{
    //Maps the identity related objects to the databse.
    public class Apply_For_Jobs_MVCIdentityContext : IdentityDbContext<IdentityUser>
    {
        public Apply_For_Jobs_MVCIdentityContext(DbContextOptions<Apply_For_Jobs_MVCIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
