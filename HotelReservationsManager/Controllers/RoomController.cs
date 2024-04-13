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
	public class RoomController : Controller
    {
        private readonly ApplicationDbContext DbContext;
		private readonly UserManager<User> UserManager;
		public RoomController(ApplicationDbContext dbContext, UserManager<User> userManger)
        {
            DbContext = dbContext;
            UserManager = userManger;
        }
        public IActionResult Index()
        {
            return View();
        }
		[Authorize(Roles = "Admin")]
		public IActionResult Add()
        {
            return View();
        }
		[Authorize(Roles = "Admin")]
		[HttpPost]
        public async Task<IActionResult> Add(Room Model)
        {
            Room Room = new Room();
            if (Model.RoomNumber == "" || Model.Capacity < 0 || Model.PricePerAdultBed <= 0 || Model.PricePerChildBed <= 0)
            {
				return RedirectToAction("List", "Room");
			}
            Room.RoomNumber = Model.RoomNumber;
            Room.Type = Model.Type;
            Room.Capacity = Model.Capacity;
            Room.PricePerChildBed = Model.PricePerChildBed;
            Room.PricePerAdultBed = Model.PricePerAdultBed;
            DbContext.Rooms.Add(Room);
            await DbContext.SaveChangesAsync();
            return RedirectToAction("List", "Room");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "Room");
			}
			return View(await DbContext.Rooms.ToListAsync());
        }
		[Authorize(Roles = "Admin")]
		[HttpGet]
        public async Task<IActionResult> Edit(Guid ID)
        {
            Room Client = await DbContext.Rooms.FindAsync(ID);
            return View(Client);
        }
		[Authorize(Roles = "Admin")]
		[HttpPost]
        public async Task<IActionResult> Edit(Room Model)
        {
			if (Model.RoomNumber == "" || Model.Capacity < 0 || Model.PricePerAdultBed <= 0 || Model.PricePerChildBed <= 0)
			{
				return RedirectToAction("List", "Room");
			}
			Room Room = await DbContext.Rooms.FindAsync(Model.ID);
            if (Room is not null)
            {
                Room.RoomNumber = Model.RoomNumber;
                Room.Type = Model.Type;
                Room.Capacity = Model.Capacity;
                Room.PricePerChildBed = Model.PricePerChildBed;
                Room.PricePerAdultBed = Model.PricePerAdultBed;
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Room");
        }
		[Authorize(Roles = "Admin")]
		[HttpPost]
        public async Task<IActionResult> Delete(Guid ID)
        {
            Room Room = await DbContext.Rooms.FindAsync(ID);
            if (Room is not null)
            {
                DbContext.Rooms.Remove(Room);
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Room");
        }
    }
}
