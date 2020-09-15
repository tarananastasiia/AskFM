using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AskFM.Models;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AskFM.Controllers
{
    [Route("likes")]
    public class LikesController : Controller
    {
        private readonly ApplicationContext _context;
        public LikesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(int questionId, string whoWasLikedId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Find(userId);
            user.Likes.Add(new Like {UserId = user.Id,QuestionId=questionId});
            _context.SaveChanges();
            return LocalRedirect($"~/question/page?userId={whoWasLikedId}");
        }
    }
}