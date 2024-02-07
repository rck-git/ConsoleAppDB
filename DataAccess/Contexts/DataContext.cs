using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Contexts;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }


    // Name of the entity , table name 

    public virtual DbSet<AdressEntity> Adresses { get; set; }
    public virtual DbSet<CategoryEntity> Categories { get; set; }
    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<ProductEntity> Products { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<RoleEntity>()
            .HasIndex(x => x.RoleName)
            .IsUnique();

        modelBuilder.Entity<CategoryEntity>()
            .HasIndex(x => x.CategoryName)
            .IsUnique();

        modelBuilder.Entity<ProductEntity>()
           .HasIndex(x => x.Title)
           .IsUnique();

        modelBuilder.Entity<AdressEntity>()
            .HasIndex(x => x.StreetName);

    }

}
