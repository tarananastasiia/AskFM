using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace AskFM.Controllers
{
    [Route("user-question")]
    public class QuestionController : Controller
    {
        private readonly ApplicationContext _context;

        public QuestionController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            return View();
        }
        [HttpPost("{userId}")]
        public IActionResult Get(QuestionDto questionDto, string userId)
        {
            _context.Questions.Add(new Question()
            {
                AnswerUserId = userId,
                QuestionUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Text = questionDto.Text,
                Answer = questionDto.Answer,

            });
            _context.SaveChanges();
            return LocalRedirect($"~/user-page/{userId}");
        }

        [HttpGet("un-answeredquestions")]
        public IActionResult UnansweredQuestions(QuestionDto questionDto)
        {
            //questionDto.AnswerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = new UserPageDTO();
            string userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var models = _context.Questions
                .Where(x => x.AnswerUserId==userId).ToList();
            dto.Questions = models.Select(x => new QuestionDto()
            {
                QuestionUserId=x.QuestionUserId,
                Text = x.Text,
                AnswerUserId = x.AnswerUserId
        }).ToList();

            return View("UnansweredQuestions", dto);
        }
    }
}