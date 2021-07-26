using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllersTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllersTest.Controllers
{
    public class PanelController : Controller
    {
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;

        public PanelController(
            SignInManager<WebAppUser> signInManager,
            UserManager<WebAppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public IActionResult Control()
        {
            return View(_userManager.Users);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(IList<string> checkboxes)
        {
            if (User.Identity != null)
            {
                WebAppUser user;
                try { user = _userManager.Users.First(x => x.UserName == User.Identity.Name); }
                catch (Exception e) { user = null; }
                
                if (user != null && user.Blocked == false)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        foreach (var t in checkboxes)
                            await _userManager.DeleteAsync(_userManager.Users.First(x => x.Email == t));

                        if (checkboxes.Any(t => t == user.Email))
                        {
                            await _signInManager.SignOutAsync();
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
                return RedirectToAction("Index", "Home");

            return View("Control", _userManager.Users);
        }
        
        public async Task<IActionResult> Block(IList<string> checkboxes)
        {
            if (User.Identity != null)
            {
                WebAppUser user;
                try { user = _userManager.Users.First(x => x.UserName == User.Identity.Name); }
                catch (Exception e) { user = null; }
                
                if (user != null && user.Blocked == false)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        foreach (var t in checkboxes)
                        {
                            if (_userManager.Users.Any(x => x.Email == t))
                            {
                                var u = _userManager.Users.First(x => x.Email == t);
                                u.Blocked = true;
                                
                                await _userManager.UpdateAsync(user);
                            }
                        }

                        if (checkboxes.Any(t => t == user.Email))
                        {
                            await _signInManager.SignOutAsync();
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
                return RedirectToAction("Index", "Home");

            return View("Control", _userManager.Users);
        }
        
        public async Task<IActionResult> UnBlock(IList<string> checkboxes)
        {
            if (User.Identity != null)
            {
                WebAppUser user;
                try { user = _userManager.Users.First(x => x.UserName == User.Identity.Name); }
                catch (Exception e) { user = null; }
                
                if (user != null && user.Blocked == false)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        foreach (var t in checkboxes)
                        {
                            if (_userManager.Users.Any(x => x.Email == t))
                            {
                                var u = _userManager.Users.First(x => x.Email == t);
                                u.Blocked = false;
                                
                                await _userManager.UpdateAsync(user);
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
                return RedirectToAction("Index", "Home");

            return View("Control", _userManager.Users);
        }
    }
}