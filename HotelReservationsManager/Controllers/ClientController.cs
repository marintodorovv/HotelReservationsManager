using HotelReservationsManager.Data;
using HotelReservationsManager.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelReservationsManager.Controllers
{
	[Authorize(Roles = "User")]
	public class ClientController : Controller
    {
        private readonly ApplicationDbContext DbContext;
		private readonly UserManager<User> UserManager;
		public ClientController(ApplicationDbContext dbContext, UserManager<User> userManager)
		{
			DbContext = dbContext;
			UserManager = userManager;
		}
		public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Client Model)
        {
            User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
            {
                return RedirectToAction("List", "Client");
			}
			if (Model.Email == "" || Model.FirstName == "" || Model.LastName == "" || Model.PhoneNumber == "" ||Model.PhoneNumber.Length != 10)
            {
				return RedirectToAction("List", "Client");
			}
            Client Client = new Client();
            Client.PhoneNumber = Model.PhoneNumber;
            Client.FirstName = Model.FirstName;
            Client.LastName = Model.LastName;
            Client.Email = Model.Email;
            Client.Adult = Model.Adult;
            DbContext.Clients.Add(Client);
            await DbContext.SaveChangesAsync();
            return RedirectToAction("List", "Client");
        }
        [HttpGet]
        public async Task<IActionResult> List(List<Client> Clients)
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "Client");
			}
			return View(await DbContext.Clients.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid ID)
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "Client");
			}
			Client Client = await DbContext.Clients.FindAsync(ID);
            return View(Client);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Client Model)
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "Client");
			}
			if (Model.Email == "" || Model.FirstName == "" || Model.LastName == "" || Model.PhoneNumber == "" || Model.PhoneNumber.Length != 10)
			{
				return RedirectToAction("List", "Client");
			}
			Client Client = await DbContext.Clients.FindAsync(Model.ID);
            if (Client is not null)
            {
                Client.PhoneNumber = Model.PhoneNumber;
                Client.FirstName = Model.FirstName;
                Client.LastName = Model.LastName;
                Client.Email = Model.Email;
                Client.Adult = Model.Adult;
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Client");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid ID)
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "Client");
			}
			Client Client = await DbContext.Clients.FindAsync(ID);
            if (Client is not null)
            {
                DbContext.Clients.Remove(Client);
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Client");
        }
        [HttpGet]
        public async Task<IActionResult> Reservations(Guid ID)
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "Client");
			}
			List<Reservation> Reservations = await DbContext.Reservations.Include(x => x.Room).Include(x => x.User).Include(x => x.Clients).ToListAsync();
            Reservations = Reservations.Where(x => x.Clients.Contains(DbContext.Clients.Find(ID))).ToList();
            return View(Reservations);
		}
    }
}
