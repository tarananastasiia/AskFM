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

            //context.Questions.Add(new Question()
            //{
            //    Answer = "wefwef",
            //    AnswerUserId = context.Users.First().Id,
            //    IsAnonimized = false,
            //    QuestionUserId = null,
            //    Text = "kdfghekfhwekf hwefjkl hlkj???"

            //});
            //context.SaveChanges();
        }

        [HttpGet("{id}")]
        public IActionResult Index(string id)
        {
            //    if (id == null)
            //        id = _context.Users.First().Id;
            id = id ?? _context.Users.First().Id;

            var dto = new UserPageDTO();

            var models = _context.Questions
                .Include(x => x.AnswerUser)
                .Where(x => x.AnswerUserId == id).ToList();

            var user = _context.Users.Find(id);

            dto.User.Name = user.UserName;

            dto.User.Id = id;
            dto.Questions = models.Select(x => new QuestionDto()
            {
                Answer = x.Answer,
                Text = x.Text,
                AnswerUserName = x.AnswerUser?.UserName

            }).ToList();

            return View("Page", dto);
        }
        //[HttpGet ("{id}/{questionid}")]
        //public IActionResult Index (string questionid)
        //{

        //}
    }
}