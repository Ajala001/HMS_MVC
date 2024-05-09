using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HMSMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
      
		public AccountController(IUserService userService, UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userService = userService;
			_userManager = userManager;
            _signInManager = signInManager;
		}
		public IActionResult Registration()
		{
			return View();
		}

        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationPage(RegistrationModel registrationModel)
        {
            var result = await _userService.CreateUserAsync(registrationModel);
            if(!result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost("Login")]
		public async Task<IActionResult> LoginPage(LoginModel loginModel)
		{
            if(!ModelState.IsValid)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var result = await _userService.PasswordSignInAsync(loginModel);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return NotFound("Invalid Credentials");
        }
    }
}
