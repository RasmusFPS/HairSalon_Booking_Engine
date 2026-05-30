
using HairSalon_Booking_Engine.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSalon_Booking_Engine
{
    public class HairSalonDBContext : DbContext
    {
        public HairSalonDBContext(DbContextOptions<HairSalonDBContext> options) : base(options)
        {
            
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Stylist> Stylist { get; set; }
        public DbSet<Treatment> Treatments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.Seed(modelBuilder);
        }
    }
}
