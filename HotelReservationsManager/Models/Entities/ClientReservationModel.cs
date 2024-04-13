namespace HotelReservationsManager.Models.Entities
{
	public class ClientReservationModel
	{
		public Guid Id { get; set; }
		public List<Client> Clients { get; set; }

	}
}
