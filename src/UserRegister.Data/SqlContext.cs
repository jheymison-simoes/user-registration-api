using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;
using UserRegister.Business.Models;
using UserRegister.Business.EntityModels;

namespace UserRegister.Data;

public class SqlContext : DbContext
{
    public SqlContext(DbContextOptions<SqlContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<UserPhone> UserPhones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlContext).Assembly);
        modelBuilder.ToSnakeCase();
        modelBuilder.HasSequence<long>("UserSequence").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<long>("AddressSequence").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<long>("UserPhoneSequence").StartsAt(1).IncrementsBy(1);
        
        base.OnModelCreating(modelBuilder);
    }
}