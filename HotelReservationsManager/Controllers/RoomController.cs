using HotelReservationsManager.Data;
using HotelReservationsManager.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelReservationsManager.Controllers
{
	[Authorize(Roles = "User")]
	public class RoomController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        public RoomController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
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
