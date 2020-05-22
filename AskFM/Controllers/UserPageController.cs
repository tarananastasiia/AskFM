using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskFM.Controllers
{
    [Route("user-page")]
    public class UserPageController : Controller
    {
        private readonly ApplicationContext _context;

        public UserPageController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Index(string id, int pageNumber = 1)
        {

            id = id ?? _context.Users.First().Id;
            int pageSize = 3;

            var dto = new UserPageDTO();
            dto.QuestionsCount = _context.Questions.Count(x => x.AnswerUserId == id && x.Answer != null);
            var models = _context.Questions
                .Include(x => x.AnswerUser)
                .Where(x => x.AnswerUserId == id && x.Answer != null)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            var user = _context.Users.Find(id);

            dto.User.Name = user.UserName;
            dto.PageSize = pageSize;
            dto.User.Id = id;
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