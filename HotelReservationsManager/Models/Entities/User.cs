using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationsManager.Models.Entities
{
    public class User : IdentityUser
    {
		public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public bool Active { get; set; }
        public DateTime FireDate { get; set; }
    }
}
