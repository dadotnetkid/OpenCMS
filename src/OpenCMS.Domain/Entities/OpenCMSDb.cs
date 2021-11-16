using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OpenCMS.Domain.Entities
{
    public class OpenCMSDb : DbContext
    {
        public OpenCMSDb(DbContextOptions<OpenCMSDb> options) :base(options)
        {
            
        }
        public DbSet<Agents> Agents { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<CardFiles> CardFiles{ get; set; }
        public DbSet<Accounts> Accounts{ get; set; }
        public DbSet<Catalogs> Catalogs { get; set; }
        public DbSet<Inventories> Inventories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Inventories>(e =>
            {
                e.HasKey(k => k.Id);
            });
            builder.Entity<Catalogs>(e =>
            {
                e.HasKey(k => k.Id);
            });
            builder.Entity<Accounts>(e =>
            {
                e.HasKey(k => k.Id);
            });
            builder.Entity<CardFiles>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedBy);
            });

            builder.Entity<Users>(e =>
            {
                e.HasKey(x => x.Id);
            });
            builder.Entity<Agents>(e =>
            {
                e.HasKey(x => x.Id);
            });
            builder.Entity<Users>(e =>
            {
                e.HasKey(x => x.Id);
            });
            builder.Entity<Roles>(e =>
            {
                e.HasKey(x => x.Id);
            });
         builder.Entity<Users>().HasMany(x => x.Roles).WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>("UsersInRoles",
                    b=>b.HasOne<Roles>().WithMany().HasForeignKey("RoleId"),
                    b=>b.HasOne<Users>().WithMany().HasForeignKey("UserId")
                    
                    );
            base.OnModelCreating(builder);
        }
    }
}
