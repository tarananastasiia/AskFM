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
        IQuestionService _questionService;

        public QuestionController( IQuestionService questionService)
        {
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
            return View("Answer", _questionService.UnansweredQuestionsDto(userId, userName));
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
            string questionName = User.FindFirstValue(ClaimTypes.Name);
            return View("Page", _questionService.PageDTO(userId,questionName,pageNumber,pageSize));
        }

    }
}