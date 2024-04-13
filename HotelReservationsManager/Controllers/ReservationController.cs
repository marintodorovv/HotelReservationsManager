using HotelReservationsManager.Data;
using HotelReservationsManager.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelReservationsManager.Controllers
{
	[Authorize(Roles = "User")]
	public class ReservationController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        private readonly UserManager<User> UserManager;
        public ReservationController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            DbContext = dbContext;
            UserManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ReservationViewModel Model)
        {
            Reservation Reservation = new Reservation();
			if (Model.AccommodationDate > Model.LeaveDate)
			{
				return RedirectToAction("List", "Reservation");
			}
			Reservation.User = await UserManager.GetUserAsync(User);
            Reservation.Room = DbContext.Rooms.ToList().Find(x => x.RoomNumber == Model.Room);
            Reservation.Clients = new List<Client> { DbContext.Clients.ToList().Find(x => x.Email == Model.Client) };
            Reservation.AccommodationDate = Model.AccommodationDate;
            Reservation.LeaveDate = Model.LeaveDate;
            Reservation.Breakfast = Model.Breakfast;
            Reservation.AllInclusive = Model.AllInclusive;
            decimal Sum = 0;
            for (int i = 0; i < (Reservation.LeaveDate - Reservation.AccommodationDate).Days; i++)
            {
				foreach (Client Client in Reservation.Clients)
				{
					if (Client.Adult)
					{
						Sum += Reservation.Room.PricePerAdultBed;
					}
					else
					{
						Sum += Reservation.Room.PricePerChildBed;
					}
				}
			}
            if (Reservation.Breakfast)
            {
                Sum += 50;
            }
            if (Reservation.AllInclusive)
            {
                Sum += 100;
            }
            Reservation.Sum = Sum;
            DbContext.Reservations.Add(Reservation);
            await DbContext.SaveChangesAsync();
            return RedirectToAction("List", "Reservation");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<Reservation> Reservations = await DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).ToListAsync();
			return View(await DbContext.Reservations.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid ID)
        {
            Reservation Reservation = await DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).FirstOrDefaultAsync(x => x.ID == ID);
            ReservationViewModel Model = new ReservationViewModel();
            Model.AllInclusive = Reservation.AllInclusive;
            Model.Room = Reservation.Room.RoomNumber;
            Model.LeaveDate = Reservation.LeaveDate;
            Model.AccommodationDate = Reservation.AccommodationDate;
            Model.Breakfast = Reservation.Breakfast;
            Model.Client = Reservation.Clients.FirstOrDefault().Email;
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ReservationViewModel Model)
        {
            Reservation Reservation = await DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).FirstOrDefaultAsync(x => x.ID == Model.Id);
            if (Reservation is not null)
            {
                if (Model.AccommodationDate > Model.LeaveDate)
                {
					return RedirectToAction("List", "Reservation");
				}
                Reservation.Room = DbContext.Rooms.ToList().Find(x => x.RoomNumber == Model.Room);
                Reservation.AccommodationDate = Model.AccommodationDate;
                Reservation.LeaveDate = Model.LeaveDate;
                Reservation.Breakfast = Model.Breakfast;
                Reservation.AllInclusive = Model.AllInclusive;
                decimal Sum = 0;
                for (int i = 0; i < (Reservation.LeaveDate - Reservation.AccommodationDate).Days; i++)
                {
                    foreach (Client Client in Reservation.Clients)
                    {
                        if (Client.Adult)
                        {
                            Sum += Reservation.Room.PricePerAdultBed;
                        }
                        else
                        {
                            Sum += Reservation.Room.PricePerChildBed;
                        }
                    }
                }
                if (Reservation.Breakfast)
                {
                    Sum += 50;
                }
                if (Reservation.AllInclusive)
                {
                    Sum += 100;
                }
                Reservation.Sum = Sum;
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Reservation");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid ID)
        {
            Reservation Reservation = await DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).FirstOrDefaultAsync(x => x.ID == ID);
            if (Reservation is not null)
            {
                DbContext.Reservations.Remove(Reservation);
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Reservation");
        }
        [HttpGet]
        public async Task<IActionResult> Clients(Guid ID)
        {
			Reservation Reservation = await DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).FirstOrDefaultAsync(x => x.ID == ID);
			List<Client> Clients = DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).Where(x => x.ID == ID).ToList().FirstOrDefault().Clients;
            return View(new ClientReservationModel { Clients = Clients, Id = ID });
		}
        [HttpGet]
		public async Task<IActionResult> AddClient(Guid ID)
		{
            return View(new ClientViewModel { Guid = ID, Email = ""});
		}
		[HttpPost]
		public async Task<IActionResult> AddClient(ClientViewModel ViewModel)
		{
			Reservation Reservation = await DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).FirstOrDefaultAsync(x => x.ID == ViewModel.Guid);
            Reservation.Clients.Add(DbContext.Clients.ToList().FirstOrDefault(x => x.Email == ViewModel.Email));
            await DbContext.SaveChangesAsync();
			return RedirectToAction("List", "Reservation");
        }
	}
}
