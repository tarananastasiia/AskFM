using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.Services;
using AskFM.Services.Contracts;
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
        private readonly ApplicationContext _context;
        private readonly IImageService _imageService;
        public UserController(ApplicationContext context,
            IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet("image")]
        public IActionResult GetImage(string userId)
        {
            var image = _imageService.GetContent(userId);
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