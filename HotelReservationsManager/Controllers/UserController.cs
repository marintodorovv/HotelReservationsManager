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
        public async Task<IActionResult> Add()
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "User");
			}
			return View();
        }
        [HttpPost]
		public async Task<IActionResult> Add(UserViewModel Model)
		{
            if (Model.Email == "" || Model.MiddleName == "" || Model.LastName == "" || Model.PhoneNumber == "" || Model.EGN == "" || Model.EGN.Length != 10 || Model.PhoneNumber.Length != 10 )
            {
				return RedirectToAction("List", "User");
			}
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
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "User");
			}
			return View(await DbContext.Users.ToListAsync());
		}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid ID)
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "User");
			}
			User Usera = await DbContext.Users.FindAsync(ID.ToString());
            return View(Usera);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User Model)
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "User");
			}
			if (Model.Email == "" || Model.MiddleName == "" || Model.LastName == "" || Model.PhoneNumber == "" || Model.EGN == "" || Model.EGN.Length != 10 || Model.PhoneNumber.Length != 10)
			{
				return RedirectToAction("List", "User");
			}
			User Usera = await DbContext.Users.FindAsync(Model.Id.ToString());
            if (Usera is not null)
            {
                Usera.FireDate = Model.FireDate;
                Usera.FirstName = Model.FirstName;
                Usera.LastName = Model.LastName;
                Usera.MiddleName = Model.MiddleName;
                Usera.EGN = Model.EGN;
                Usera.Email = Model.Email;
                Usera.NormalizedEmail = Model.Email.ToUpper();
                Usera.UserName = Model.Email;
                Usera.NormalizedUserName = Model.Email.ToUpper();
                Usera.PhoneNumber = Model.PhoneNumber;
                Usera.Active = Model.Active;
                Usera.HireDate = Model.HireDate;
                Usera.FireDate = Model.FireDate;
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "User");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid ID)
        {
			User Current = await UserManager.GetUserAsync(User);
			if (!Current.Active)
			{
				return RedirectToAction("List", "User");
			}
			User Usera = await DbContext.Users.FindAsync(ID.ToString());
            if (Usera is not null)
            {
                DbContext.Users.Remove(Usera);
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "User");
        }
    }
}
