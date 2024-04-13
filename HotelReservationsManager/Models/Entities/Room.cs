using HotelReservationsManager.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.Entities
{
    public enum Type
    {
        Double,
        TwoSingles,
        Apartment,
        Penthouse,
        Maisonette
    }
    public class Room
    {
		public Guid ID { get; set; }
		[Required]
		public int Capacity { get; set; }
        public Type Type { get; set; }
		[Required]
		public bool Available
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer("Server=DESKTOP-64FVMNQ\\SQLEXPRESS;Database=HotelReservationsManager;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
                ApplicationDbContext DbContext = new ApplicationDbContext(optionsBuilder.Options);
                foreach (var item in DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).ToList())
				{
                    if (item.Room.ID == ID && item.LeaveDate > DateTime.Now)
                    {
                        return false;
                    }
                }
				return true;
			}
			}
		[Required]
		public decimal PricePerAdultBed { get; set; }
		[Required]
		public decimal PricePerChildBed { get; set; }
		[Required]
		public string RoomNumber { get; set; }
    }

}
