using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskFM.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private readonly ApplicationContext _context;
        public UserController(IWebHostEnvironment appEnvironment, ApplicationContext context)
        {
            _appEnvironment = appEnvironment;
            _context = context;
        }

        [HttpGet("image")]
        public IActionResult GetImage(string userId)
        {
            var user = _context.Users
                .Include(x => x.Images)
                .FirstOrDefault(x => x.Id == userId);

            var imageMetaData = user.Images;
            var path = imageMetaData.First().Path; 

            var image = System.IO.File.OpenRead(_appEnvironment.WebRootPath+path);
            return File(image, "image/jpeg");
        }

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