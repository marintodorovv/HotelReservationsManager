using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.Entities
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }
		[Required]
		public string Room { get; set; }
		[Required]
		public string Client { get; set; }
		[Required]
		public DateTime AccommodationDate { get; set; }
		[Required]
		public DateTime LeaveDate { get; set; }
		[Required]
		public bool Breakfast { get; set; }
		[Required]
		public bool AllInclusive { get; set; }
    }
}
