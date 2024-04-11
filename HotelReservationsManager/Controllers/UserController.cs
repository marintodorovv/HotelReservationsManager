using HotelReservationsManager.Areas.Identity.Pages.Account;
using HotelReservationsManager.Data;
using HotelReservationsManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;

namespace HotelReservationsManager.Controllers
{
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
            return View();
		}
	}
}
