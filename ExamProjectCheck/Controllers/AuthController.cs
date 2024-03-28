using ExamProjectCheck.Data;
using ExamProjectCheck.Models.Entity;
using ExamProjectCheck.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamProjectCheck.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly examDbContext _examDbContext;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, examDbContext examDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _examDbContext = examDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModelss request)
        {
            var userChek = await _userManager.FindByEmailAsync(request.Email);
            if (ModelState.IsValid)

            {
                if (userChek is null)
                {
                    AppUser user = new()
                    {
                        FullName = request.FullName,
                        Email = request.Email,
                        UserName = request.Email
                    };
                    var result = await _userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Login");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelss request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAll", "Exam");
                    }
                    ModelState.AddModelError("All", "email or password not valid");

                }


            }
            return View(request);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
