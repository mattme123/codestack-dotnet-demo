using codestackdemo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codestackdemo.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Defaults

            modelBuilder.Entity<User>().Property(x => x.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<User>().Property(x => x.Created).HasDefaultValueSql("GetDate()");

            // Filters

            modelBuilder.Entity<User>().HasQueryFilter(x => x.IsDeleted == false);

            // Seed Data

            modelBuilder.Entity<User>()
                .HasData(
                    new User { UserId = 1, FirstName = "Matthew", LastName = "Hicks", Email = "csa_mhicks@sjcoe.net", RoleId = 1 }
                );

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { RoleId = 1, Name = "Admin" },
                    new Role { RoleId = 2, Name = "Standard" }
                );
        }
    }
}
