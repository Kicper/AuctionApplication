using AuctionApplication.Models;
using AuctionApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegistration(UserModel user)
        {
            SecurityService securityService = new SecurityService();
            

            if(String.Equals(user.Password, user.RepeatPassword) && !securityService.Exists(user))
            {
                securityService.AddUser(user);
                TempData["userId"] = securityService.GetId(user);
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return View("RegistrationFailure", user);
            }
        }
    }
}
