using EventEase.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Event -> Venue (optional: an event can exist without a venue yet)
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Venue)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.SetNull);

            // Booking -> Event (restrict delete so an event with bookings cannot be removed)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking -> Venue (restrict delete so a venue with bookings cannot be removed)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Venue)
                .WithMany(v => v.Bookings)
                .HasForeignKey(b => b.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data so the app has something to show immediately after the first migration.
            modelBuilder.Entity<Venue>().HasData(
                new Venue { VenueId = 1, VenueName = "Riverside Hall", Location = "12 River Road, Pretoria", Capacity = 250, ImageUrl = "https://picsum.photos/seed/riverside/600/400" },
                new Venue { VenueId = 2, VenueName = "Garden Pavilion", Location = "45 Botanical Ave, Johannesburg", Capacity = 120, ImageUrl = "https://picsum.photos/seed/garden/600/400" },
                new Venue { VenueId = 3, VenueName = "Skyline Conference Centre", Location = "1 Corporate Blvd, Sandton", Capacity = 500, ImageUrl = "https://picsum.photos/seed/skyline/600/400" }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event { EventId = 1, EventName = "Smith Wedding", EventDate = new DateTime(2026, 11, 14), Description = "Wedding reception for 150 guests.", VenueId = 2, ImageUrl = "https://picsum.photos/seed/wedding/600/400" },
                new Event { EventId = 2, EventName = "Annual Tech Conference", EventDate = new DateTime(2026, 10, 3), Description = "Two-day technology conference.", VenueId = 3, ImageUrl = "https://picsum.photos/seed/conference/600/400" },
                new Event { EventId = 3, EventName = "Charity Gala", EventDate = new DateTime(2027, 1, 20), Description = "Fundraising dinner, venue still to be confirmed.", VenueId = null, ImageUrl = "https://picsum.photos/seed/gala/600/400" }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking { BookingId = 1, EventId = 1, VenueId = 2, BookingDate = new DateTime(2026, 9, 1) },
                new Booking { BookingId = 2, EventId = 2, VenueId = 3, BookingDate = new DateTime(2026, 9, 3) }
            );
        }
    }
}
