using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }
        [HttpPost]
        public IActionResult Create(QuestionDto questionDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Questions.Add(new Question()
            {
                AnswerUserId = userId,
                QuestionUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Text = questionDto.Text,
                Answer = questionDto.Answer,

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
                Id = x.Id
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
                .Where(x => x.AnswerUserId == userId && x.Answer != null)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            var user = _context.Users.Find(userId);

            //dto.User.Name = user.UserName;
            dto.PageSize = pageSize;
            dto.User.Id = userId;
            dto.PageNumber = pageNumber;
            dto.Questions = models.Select(x => new QuestionDto()
            {
                Answer = x.Answer,
                Text = x.Text,
                AnswerUserName = x.AnswerUser?.UserName
            }).ToList();
            return View("Page", dto);
        }

    }
}