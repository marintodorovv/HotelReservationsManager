using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.Entities
{
    public class Client
    {
        public Guid ID { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public bool Adult { get; set; }
		public virtual List<Reservation> Reservations { get; set; }

    }
}
