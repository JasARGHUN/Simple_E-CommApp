using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleTemplate_Shop.Models.ViewModels;
using System.Collections.Generic;

namespace SimpleTemplate_Shop.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<AppAddress> AppAddresses { get; set; }
        public DbSet<AppSocialAddress> AppSocialAddresses { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
