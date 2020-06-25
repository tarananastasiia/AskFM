using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskFM.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;
        public UserController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet("users")]
        public IActionResult SearchUsers(string name)
        {
            List<User> users = new List<User>();
            if (!string.IsNullOrEmpty(name))
            {
                users = _context.Users.Where(p => p.Email.Contains(name)).ToList();
            }

            UsersSearch viewModel = new UsersSearch
            {
                Users = users.ToList(),
                Name = name
            };
            return View(viewModel);
        }
    }
}