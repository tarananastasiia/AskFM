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
            return LocalRedirect($"~/user-page/{userId}");
        }

        [HttpGet("un-answeredquestions")]
        public IActionResult UnansweredQuestions()
        {
            var dto = new UserPageDTO();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var models = _context.Questions
                .Where(x => x.AnswerUserId == userId && x.Answer==null).ToList();
            dto.Questions = models.Select(x => new QuestionDto()
            {
                QuestionUserId = x.QuestionUserId,
                Text = x.Text,
                AnswerUserId = x.AnswerUserId,
                Id = x.Id
            }).ToList();
            return View("Answer", dto);
        }
        //[HttpGet("un-answeredquestions/{Id}")]
        //public IActionResult Answer(int id)
        //{
        //    var model = _context.Questions
        //        .First(x => x.Id == id);
        //    var question = new QuestionDto()
        //    {
        //        QuestionUserId = model.QuestionUserId,
        //        Text = model.Text,
        //        AnswerUserId = model.AnswerUserId,
        //        Id = id,
        //    };
        //    return View("Answer", question);
        //}

        [HttpPost("{Id}")]
        public IActionResult Answer([FromForm]QuestionDto questionDto, int id)
        {
            var files = HttpContext.Request.Form;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var question = _context.Questions.First(x=>x.Id==id);
            question.Answer = questionDto.Answer;
            _context.SaveChanges();
            return LocalRedirect($"~/user-page/{userId}");
        }
    }
}