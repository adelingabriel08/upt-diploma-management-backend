using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UPT.Diploma.Management.Domain.Models;

namespace UPT.Diploma.Management.Persistence.Database;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Faculty> Faculties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Faculty>()
            .Property(x => x.Name).HasMaxLength(150)
            .IsRequired()
            .IsUnicode();

        modelBuilder.Entity<Faculty>()
            .Property(x => x.ShortName).HasMaxLength(5)
            .IsRequired()
            .IsUnicode();

        modelBuilder.Entity<Faculty>()
            .HasIndex(x => x.ShortName)
            .IsUnique();
    }
}