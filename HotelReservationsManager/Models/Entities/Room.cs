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
        public int Capacity { get; set; }
        public Type Type { get; set; }
        public bool Available { get; set; }
        public decimal PricePerAdultBed { get; set; }
        public decimal PricePerChildBed { get; set; }
        public string RoomNumber { get; set; }
    }

}
