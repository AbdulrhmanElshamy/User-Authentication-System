using Microsoft.AspNetCore.Mvc;
using User_Authentication_System.constnet;
using User_Authentication_System.Repos.AuthRepos;
using User_Authentication_System.View_Models;

namespace User_Authentication_System.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAuthRepo _authRepo;

        public AccountController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            var res = await _authRepo.RegisterAsync(model);

            if(res.Errors is not null)
            {
                ModelState.AddModelError("", res.Errors);
                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var res = await _authRepo.LoginAsync(model);

            if (res.Errors is not null)
            {
                ModelState.AddModelError("", res.Errors);
                return View(model);
            }

            return RedirectToAction(nameof(Index), "Home");;
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {

            await _authRepo.LogOutAsync();

            return RedirectToAction(nameof(Login)); ;
        }


        public async Task<IActionResult> AllowUser(string user)
        {
            return Json(await _authRepo.AllowUser(user));
        }

        public async Task<IActionResult> AllowEmail(string email)
        {
            return Json(await _authRepo.AllowEmail(email));
        }

    }
}
