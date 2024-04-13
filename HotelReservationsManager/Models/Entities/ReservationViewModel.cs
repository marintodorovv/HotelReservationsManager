namespace HotelReservationsManager.Models.Entities
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }
        public string Room { get; set; }
        public string Client { get; set; }
        public DateTime AccommodationDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
    }
}
