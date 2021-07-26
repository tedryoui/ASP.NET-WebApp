using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControllersTest.Models;
using Microsoft.AspNetCore.Identity;
 
namespace ControllersTest.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<WebAppUser> _userManager;
        private readonly SignInManager<WebAppUser> _signInManager;
 
        public AuthController(UserManager<WebAppUser> userManager, SignInManager<WebAppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if(ModelState.IsValid)
            {
                WebAppUser user = new WebAppUser {UserName = model.Name, Email = model.Email, Blocked = false};
                
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    
                    user.RegDate = DateTime.Now;
                    user.LogDate = DateTime.Now;
                    await _userManager.UpdateAsync(user);
                    
                    return RedirectToAction("Control", "Panel");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.Users.Any(x => x.UserName == model.Email) &&
                    _userManager.Users.First(x=>x.UserName == model.Email).Blocked == false)
                {
                    var result =
                        await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var user = _userManager.Users.First(x => x.UserName == model.Email);
                        user.LogDate = DateTime.Now;
                        await _userManager.UpdateAsync(user);
                        
                        return RedirectToAction("Control", "Panel");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь заблокирован!");
                }
            }
            return View(model);
        }
    }
}

/*"default" : "Data Source=localhost;Initial Catalog=WebAppDB;Integrated Security=True;MultipleActiveResultSets=True",*/