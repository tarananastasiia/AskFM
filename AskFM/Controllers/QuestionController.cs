using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace AskFM.Controllers
{
    [Route("question")]
    public class QuestionController : Controller
    {
        private readonly ApplicationContext _context;

        public QuestionController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(QuestionDto questionDto, string userId)
        {
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            _context.Questions.Add(new Question()
            {
                AnswerUserId = userId,
                QuestionUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Text = questionDto.Text,
                Answer = questionDto.Answer,
                IsAnonimized=questionDto.IsAnonimized,
            });
            _context.SaveChanges();
            return LocalRedirect($"~/question/page");
        }

        [HttpGet("un-answeredquestions")]
        public IActionResult UnansweredQuestions()
        {
            var dto = new UserPageDTO();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var models = _context.Questions
                .Where(x => x.AnswerUserId == userId && x.Answer == null).ToList();
            dto.Questions = models.Select(x => new QuestionDto()
            {
                QuestionUserId = x.QuestionUserId,
                Text = x.Text,
                AnswerUserId = x.AnswerUserId,
                Id = x.Id,
                IsAnonimized=x.IsAnonimized,
                QuestionUserName = User.FindFirstValue(ClaimTypes.Name),
            }).ToList();
            return View("Answer", dto);
        }

        [HttpPost("{Id}")]
        public IActionResult Answer([FromForm]QuestionDto questionDto, int id)
        {
            var files = HttpContext.Request.Form;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var question = _context.Questions.First(x => x.Id == id);
            question.Answer = questionDto.Answer;
            _context.SaveChanges();
            return LocalRedirect($"~/question/page");
        }

        [HttpGet("page")]
        public IActionResult Page(string userId, int pageNumber = 1, int pageSize = 3)
        {
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var dto = new UserPageDTO();
            dto.QuestionsCount = _context.Questions.Count(x => x.AnswerUserId == userId && x.Answer != null);
            var models = _context.Questions
                .Include(x => x.AnswerUser)
                .Include(x => x.Comments)
                .Where(x => x.AnswerUserId == userId && x.Answer != null)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            var user = _context.Users.Find(userId);

            dto.PageSize = pageSize;
            dto.User.Id = userId;
            dto.PageNumber = pageNumber;
            dto.Questions = models.Select(question => new QuestionDto()
            {
                Answer = question.Answer,
                Text = question.Text,
                AnswerUserName = question.AnswerUser?.UserName,
                Id = question.Id,
                IsAnonimized=question.IsAnonimized,
                QuestionUserName= User.FindFirstValue(ClaimTypes.Name),
                Comments = question.Comments.Select(comment => new CommentDto()
                {
                    Text = comment.Text,
                    IsAnonimized = comment.IsAnonimized,
                    UserId = comment.IsAnonimized ? null : comment.UserId,
                    UserName = comment.IsAnonimized ? null : comment.UserName,
                }).ToList()
            }).ToList();
            return View("Page", dto);
        }

    }
}