using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AskFM.Models;
using Microsoft.AspNetCore.Mvc;

namespace AskFM.Controllers
{
    [Route("subscriptions")]
    public class SubscriptionsController : Controller
    {
        private readonly ApplicationContext _context;
        public SubscriptionsController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add(string userId)
        {
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return View();
        }
    }
}
