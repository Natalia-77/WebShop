using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.AbstractConfiguration;
using WebShop.Domain.AbstractConfiguration.Products;
using WebShop.Domain.Entities.Identity;
using WebShop.Domain.Entities.Products;

namespace WebShop.Domain
{
     public class AppEFContext: IdentityDbContext<AppUser, AppRole, long, IdentityUserClaim<long>,
        AppUserRole, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
           : base(options)
        {

        }
        public DbSet<Cat> Cats { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserRoleConfig());
            builder.ApplyConfiguration(new CatConfig());
        }
    }
}
