using CitiFine.Areas.Identity.Data;
using CitiFine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Reflection.Emit;

namespace CitiFine.Areas.Identity.Data;

public class CitiFineDbContext : IdentityDbContext<CitiFineUser>
{
    public CitiFineDbContext(DbContextOptions<CitiFineDbContext> options)
        : base(options)
    { }

    // Add the Violation DbSet
    public DbSet<Violation> Violations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);


        // Role IDs
        string adminRoleId = Guid.NewGuid().ToString();
        string officerRoleId = Guid.NewGuid().ToString();
        string userRoleId = Guid.NewGuid().ToString();

        // Seed Roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = adminRoleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole { Id = officerRoleId, Name = "Officer", NormalizedName = "OFFICER" },
            new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
        );

        // User IDs
        string user2Id = Guid.NewGuid().ToString();
        string user3Id = Guid.NewGuid().ToString();

        // Hashed Password (Use ASP.NET Identity's PasswordHasher to hash a password)
        var hasher = new PasswordHasher<CitiFineUser>();
        string hashedPassword = hasher.HashPassword(null, "Test@123"); // Set a default password

        // Seed Users
        builder.Entity<CitiFineUser>().HasData(
            new CitiFineUser
            {
                Id = user2Id,
                FirstName = "User2",
                LastName = "Lastname",
                LicensePlate = "USR223",
                UserName = "user2@test.com",
                NormalizedUserName = "USER2@TEST.COM",
                Email = "user2@test.com",
                NormalizedEmail = "USER2@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = hashedPassword,
                SecurityStamp = string.Empty
            },
            new CitiFineUser
            {
                Id = user3Id,
                FirstName = "User3",
                LastName = "Lastname",
                LicensePlate = "USR323",
                UserName = "user3@test.com",
                NormalizedUserName = "USER3@TEST.COM",
                Email = "user3@test.com",
                NormalizedEmail = "USER3@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = hashedPassword,
                SecurityStamp = string.Empty
            }
        );

        // Seed User Roles (Assigning users to roles)
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = user2Id, RoleId = officerRoleId },
            new IdentityUserRole<string> { UserId = user2Id, RoleId = userRoleId },
            new IdentityUserRole<string> { UserId = user3Id, RoleId = adminRoleId },
            new IdentityUserRole<string> { UserId = user3Id, RoleId = officerRoleId }
        );

        // Seed Violation Data
        builder.Entity<Violation>().HasData(
            new Violation
            {
                ViolationId = 1,
                ViolationType = "Speeding",
                FineAmount = 100.00m,
                UserId = user2Id,
                IsPaid = false,
                DateIssued = new DateTime(2023, 1, 1)
            },
            new Violation
            {
                ViolationId = 2,
                ViolationType = "Red Light",
                FineAmount = 150.00m,
                UserId = user2Id,
                IsPaid = false,
                DateIssued = new DateTime(2023, 1, 2)
            }
        );

        // Builder for property constraints
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

// Configuration of property constraints
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<CitiFineUser>
{
    public void Configure(EntityTypeBuilder<CitiFineUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
        builder.Property(u => u.LicensePlate).HasMaxLength(7);
    }
}