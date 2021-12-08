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
        public OpenCMSDb(DbContextOptions<OpenCMSDb> options) : base(options)
        {

        }

        public DbSet<Agents> Agents { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<CardFiles> CardFiles { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Catalogs> Catalogs { get; set; }
        public DbSet<Inventories> Inventories { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionsInRoles> PermissionsInRoles { get; set; }
        public DbSet<CatalogBuyingDetails> CatalogBuyingDetails { get; set; }
        public DbSet<CatalogSellingDetails> CatalogSellingDetails { get; set; }
        public DbSet<Tenants> Tenants { get; set; }
        public DbSet<Classifications> Classifications { get; set; }
        public DbSet<ConfigurationManagements> ConfigurationManagements { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ConfigurationManagements>(e =>
            {
                e.HasKey(x => x.Id);
            });
            builder.Entity<Classifications>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasMany(x => x.Accounts).WithOne(x => x.Classifications).HasForeignKey(x => x.ClassificationId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            builder.Entity<Transactions>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasMany(x => x.SalesItems)
                    .WithOne(x => x.Sales)
                    .HasForeignKey(x => x.TransactionId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.CardFile).WithMany().HasForeignKey(x => x.CardFileId).OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<TransactionItems>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Catalogs).WithMany().HasForeignKey(x => x.CatalogId).OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Inventories>(e =>
            {
                e.HasKey(k => k.Id);
            });
            builder.Entity<Catalogs>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasMany(x => x.CatalogBuyingDetails).WithOne(x => x.Catalog).HasForeignKey(x => x.CatalogId);
                e.HasMany(x => x.CatalogSellingDetails).WithOne(x => x.Catalog).HasForeignKey(x => x.CatalogId);
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
                e.HasMany(x => x.PermissionsInRoles).WithOne().HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<PermissionsInRoles>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Users>().HasMany(x => x.Roles).WithMany(x => x.Users)
                   .UsingEntity<Dictionary<string, object>>("UsersInRoles",
                       b => b.HasOne<Roles>().WithMany().HasForeignKey("RoleId"),
                       b => b.HasOne<Users>().WithMany().HasForeignKey("UserId")

                       );
            base.OnModelCreating(builder);
        }
    }
}
