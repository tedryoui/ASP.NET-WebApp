using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllersTest.Models;
using ControllersTest.Models.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ControllersTest.Controllers
{
    
    public class HomeController : Controller
    {
        public HomeController() { }
        
        public IActionResult Index()
        {
            if (User.Identity is {IsAuthenticated: true})
                return RedirectToAction("Control", "Panel");
            
            return View();
        }
        
    }
}