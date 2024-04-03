using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelReservationsManager.Models.Entities;

namespace HotelReservationsManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        DbSet<Client> Clients { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}