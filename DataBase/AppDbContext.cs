using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TravelAgency;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => new { r.Id, r.UserId, r.TripId });

                entity.Property(r => r.Id)
                      .ValueGeneratedOnAdd();

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Trip)
                      .WithMany(t => t.Reviews)
                      .HasForeignKey(r => r.TripId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => new { b.Id, b.UserId, b.TripId });

                entity.Property(b => b.Id)
                      .ValueGeneratedOnAdd();

                entity.HasOne(b => b.User)
                      .WithMany(u => u.Bookings)
                      .HasForeignKey(b => b.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Trip)
                      .WithMany(t => t.Bookings)
                      .HasForeignKey(b => b.TripId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasOne(t => t.Destination)
                      .WithMany(d => d.Trips)
                      .HasForeignKey(t => t.DestinationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Employee)
                      .WithMany(e => e.Trips)
                      .HasForeignKey(t => t.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
