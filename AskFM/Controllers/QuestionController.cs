using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace AskFM.Controllers
{
    [Route("question")]
    public class QuestionController : Controller
    {
        private readonly ApplicationContext _context;
        IQuestionService _questionService;

        public QuestionController(ApplicationContext context, IQuestionService questionService)
        {
            _context = context;
            _questionService = questionService;
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
            string questionUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _questionService.Add(questionDto, userId, questionUserId);
            return LocalRedirect($"~/question/page?userId={userId}");
        }


        [HttpGet("un-answeredquestions")]
        public IActionResult UnansweredQuestions()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userName = User.FindFirstValue(ClaimTypes.Name);
            return View("Answer", _questionService.UnansweredQuestionsModelsToList(userId, userName));
        }

        [HttpPost("{Id}")]
        public IActionResult Answer(QuestionDto questionDto, int id)
        {
            _questionService.Answer(questionDto, id);
            return LocalRedirect($"~/question/page");
        }

        [HttpGet("page")]
        public IActionResult Page(string userId, int pageNumber = 1, int pageSize = 3)
        {
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            //var models = _context.Questions
            //                .Include(x => x.AnswerUser)
            //                .Include(x => x.Comments)
            //                .Where(x => x.AnswerUserId == userId && x.Answer != null)
            //                .Skip((pageNumber - 1) * pageSize)
            //                .Take(pageSize).ToList();

            var user = _context.Users.Find(userId);

            string questionName = User.FindFirstValue(ClaimTypes.Name);

            //var dto = new UserPageDTO();
            //dto.QuestionsCount = _context.Questions.Count(x => x.AnswerUserId == userId && x.Answer != null);
            //dto.PageSize = pageSize;
            //dto.User.Id = userId;
            //dto.PageNumber = pageNumber;
            //dto.Questions = models.Select(question => new QuestionDto()
            //{
            //    Answer = question.Answer,
            //    Text = question.Text,
            //    AnswerUserName = question.AnswerUser?.UserName,
            //    Id = question.Id,
            //    IsAnonimized = question.IsAnonimized,
            //    QuestionUserName = User.FindFirstValue(ClaimTypes.Name),
            //    Comments = question.Comments.Select(comment => new CommentDto()
            //    {
            //        QuestionId = question.Id,
            //        Text = comment.Text,
            //        IsAnonimized = comment.IsAnonimized,
            //        UserId = comment.IsAnonimized ? null : comment.UserId,
            //        UserName = comment.IsAnonimized ? null : comment.UserName,
            //    }).ToList()
            //}).ToList();
            return View("Page", dto);
        }

    }
}