using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskFM.Models;
using AskFM.ViewModels;
using System.Security.Claims;

namespace AskFM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string userId)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                return Redirect($"~/user-page/{userId}");
            }
            else
            return View();
        }
    }
}
