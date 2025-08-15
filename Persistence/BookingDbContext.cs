using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Domain;

namespace BookingService.Infrastructure;

public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> opts) : base(opts) { }

    public DbSet<Booking> Bookings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.CustomerName).HasMaxLength(200).IsRequired();
            b.Property(x => x.BookingDate).IsRequired();
        });
    }
}
