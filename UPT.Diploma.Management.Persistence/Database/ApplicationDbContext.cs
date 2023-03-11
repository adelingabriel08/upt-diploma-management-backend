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

        modelBuilder.Entity<Student>()
            .Property(x => x.StudentIdentifier).HasMaxLength(8)
            .IsRequired();

        modelBuilder.Entity<Student>()
            .HasIndex(x => x.StudentIdentifier)
            .IsUnique();
        
        modelBuilder.Entity<Student>()
            .Property(x => x.Specialization).HasMaxLength(200)
            .IsRequired()
            .IsUnicode();
        
        modelBuilder.Entity<Student>()
            .Property(x => x.Profile).HasMaxLength(200)
            .IsRequired()
            .IsUnicode();

        modelBuilder.Entity<Student>()
            .HasOne(x => x.User)
            .WithOne(x => x.Student)
            .HasForeignKey<Student>(x => x.UserId)
            .IsRequired();
        
        modelBuilder.Entity<Company>()
            .Property(x => x.Name).HasMaxLength(150)
            .IsRequired()
            .IsUnicode();
        
        modelBuilder.Entity<Company>()
            .Property(x => x.LogoUrl).HasMaxLength(2000)
            .IsRequired();
        
        modelBuilder.Entity<Company>()
            .Property(x => x.ShortDescription).HasMaxLength(500)
            .IsRequired()
            .IsUnicode();
        
        modelBuilder.Entity<Company>()
            .HasOne(x => x.User)
            .WithOne(x => x.Company)
            .HasForeignKey<Company>(x => x.UserId)
            .IsRequired();
        
        modelBuilder.Entity<Topic>()
            .Property(x => x.Name).HasMaxLength(200)
            .IsRequired()
            .IsUnicode();
        
        modelBuilder.Entity<Topic>()
            .Property(x => x.Description).HasMaxLength(2000)
            .IsRequired()
            .IsUnicode();
        
        modelBuilder.Entity<Topic>()
            .HasOne(x => x.Professor)
            .WithMany(x => x.Topics);
        
        modelBuilder.Entity<Topic>()
            .HasOne(x => x.Company)
            .WithMany(x => x.Topics);
        
        modelBuilder.Entity<Topic>()
            .HasOne(x => x.Faculty)
            .WithMany(x => x.Topics)
            .IsRequired();
    }
}