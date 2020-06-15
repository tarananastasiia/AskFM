using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AskFM.Controllers
{
    [Route("comment")]
    public class CommentController : Controller
    {
        private readonly ApplicationContext _context;

        public CommentController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpPost]//TODO: move id to commentdto
        public IActionResult Comments(CommentDto commentDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Comments.Add(new Comment()
            {
                QuestionId = commentDto.QuestionId,
                Text = commentDto.Text,
                IsAnonimized = commentDto.IsAnonimized,
                UserId = userId,
                UserName= User.FindFirstValue(ClaimTypes.Name),
            });
            _context.SaveChanges();
            var answerUserId = _context.Questions.Find(commentDto.QuestionId).AnswerUserId;
            return LocalRedirect($"~/question/page?userId={answerUserId}");
        }
    }
}   