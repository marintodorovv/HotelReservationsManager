namespace HotelReservationsManager.Models.Entities
{
    public class Client
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Adult { get; set; }

    }
}
