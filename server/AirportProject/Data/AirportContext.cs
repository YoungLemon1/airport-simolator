using FlightSimulator.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace FlightSimulator.Data
{
    public class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options) : base(options)
        {
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Stop> Stops { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stop1 = new Stop()
            {
                Id = 1,
                Name = "Landing1"
            };
            var stop2 = new Stop() 
            { 
                Id = 2,
                Name = "Landing2"
            };
            var stop3 = new Stop()
            {
                Id = 3,
                Name = "Landing3"
            };
            var stop4 = new Stop()
            {
                Id = 4,
                Name = "Runway"
            };
            var stop5 = new Stop()
            {
                Id = 5,
                Name = "Landing Track"
            };
            var stop6 = new Stop()
            { 
                Id = 6,
                Name = "Gate1" };
            var stop7 = new Stop()
            { 
                Id = 7, 
                Name = "Gate2" };
            var stop8 = new Stop()
            {
                Id = 8,
                Name = "Takeoff Track"
            };

            modelBuilder.Entity<Stop>().HasData(
                stop1, stop2, stop3, stop4, stop5, stop6, stop7, stop8
                );
            modelBuilder.Entity<Flight>()
                .Property(f => f.CreatedTime).HasDefaultValue(DateTime.Now);
        }
    }
}
