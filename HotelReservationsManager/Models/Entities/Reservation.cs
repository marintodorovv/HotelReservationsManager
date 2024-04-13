using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationsManager.Models.Entities
{
    public class Reservation
    {
        public Guid ID { get; set; }
		public virtual Room Room { get; set; }
		public virtual User User { get; set; }
        public virtual List<Client> Clients { get; set; }
        public DateTime AccommodationDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
        public decimal Sum { get; set; }
    }
}
