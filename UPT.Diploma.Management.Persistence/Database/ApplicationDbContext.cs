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

        modelBuilder.Entity<ApplicationUser>()
            .Property(x => x.FirstName).HasMaxLength(100)
            .IsRequired()
            .IsUnicode();
        
        modelBuilder.Entity<ApplicationUser>()
            .Property(x => x.LastName).HasMaxLength(100)
            .IsRequired()
            .IsUnicode();
        
        modelBuilder.Entity<ApplicationUser>()
            .Property(x => x.ProfilePictureUrl).HasMaxLength(2000);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(x => x.Faculty)
            .WithMany(x => x.Users);
    }
}