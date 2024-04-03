namespace HotelReservationsManager.Models.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public bool Active { get; set; }
        public DateTime FireDate { get; set; }
        public bool Admin { get; set; }

    }
}
