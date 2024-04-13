using HotelReservationsManager.Areas.Identity.Pages.Account;
using HotelReservationsManager.Data;
using HotelReservationsManager.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace HotelReservationsManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        private readonly UserManager<User> UserManager;
        public UserController(ApplicationDbContext dbContext, UserManager<User> userManager)
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
		public async Task<IActionResult> Add(UserViewModel Model)
		{
            User User = Activator.CreateInstance<User>();
            User.Active = true;
            User.HireDate = DateTime.Now;
            User.Email = Model.Email;
            User.NormalizedEmail = Model.Email.ToUpper();
            User.UserName = Model.Email;
            User.NormalizedUserName = Model.Email.ToUpper();
            User.FirstName = Model.FirstName;
            User.LastName = Model.LastName;
            User.MiddleName = Model.MiddleName;
            User.EGN = Model.EGN;
            User.PhoneNumber = Model.PhoneNumber;
            User.EmailConfirmed = true;
            User.PasswordHash = new PasswordHasher<User>().HashPassword(User, Model.Password);
            var UserStore = new UserStore<User>(DbContext);
            await UserStore.CreateAsync(User);
            await UserManager.AddToRoleAsync(User, "User");
            await DbContext.SaveChangesAsync();
            return RedirectToAction("List", "User");
        }
		[HttpGet]
		public async Task<IActionResult> List()
		{
			return View(await DbContext.Users.ToListAsync());
		}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid ID)
        {
            User User = await DbContext.Users.FindAsync(ID.ToString());
            return View(User);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User Model)
        {
            User User = await DbContext.Users.FindAsync(Model.Id.ToString());
            if (User is not null)
            {
                User.FireDate = Model.FireDate;
                User.FirstName = Model.FirstName;
                User.LastName = Model.LastName;
                User.MiddleName = Model.MiddleName;
                User.EGN = Model.EGN;
                User.Email = Model.Email;
                User.NormalizedEmail = Model.Email.ToUpper();
                User.UserName = Model.Email;
                User.NormalizedUserName = Model.Email.ToUpper();
                User.PhoneNumber = Model.PhoneNumber;
                User.Active = Model.Active;
                User.HireDate = Model.HireDate;
                User.FireDate = Model.FireDate;
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "User");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid ID)
        {
            User User = await DbContext.Users.FindAsync(ID.ToString());
            if (User is not null)
            {
                DbContext.Users.Remove(User);
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "User");
        }
    }
}
