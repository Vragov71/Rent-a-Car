using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;

namespace RentACar.Data;

/// <summary>
/// Контекст на базата данни – Entity Framework Core.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Уникалност на ЕГН
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Egn)
            .IsUnique();

        // Уникалност на имейл (вече е в Identity, но изрично за яснота)
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
