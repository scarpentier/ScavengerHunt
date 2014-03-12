using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity.EntityFramework;

namespace ScavengerHunt.Web.Models
{
    public class ScavengerHuntContext : IdentityDbContext<ApplicationUser>
    {
        public ScavengerHuntContext()
            : base("ScavengerHuntContext")
        {
            
        }

        public DbSet<Stunt> Stunts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamStunt> TeamStunts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("IdentityUsers").Property(p => p.Id);
            modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}