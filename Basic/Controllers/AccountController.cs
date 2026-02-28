using Basic.Data;
using Basic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    
    public class AccountController : Controller{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult>Register(RegisterVm vm)
        {
            if(!ModelState.IsValid) return View(vm);
            var  user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                FullName = vm.FullName  
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach(var i in result.Errors) ModelState.AddModelError("",i.Description);
                return View(vm);
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Login","Account");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(Loginvm vm, string? returnUrl = null)
        {
            if(!ModelState.IsValid) return View(vm);
            var result = await _signInManager.PasswordSignInAsync(
                userName: vm.Email,
                password: vm.Password,
                isPersistent: vm.RememberMe,
                lockoutOnFailure: true
            );
            if (!result.Succeeded)
            {
                ModelState.AddModelError("","E-posta veya parola hatalıdır!");
                return View(vm);
            }
            if(!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return RedirectToAction(returnUrl);
            
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}