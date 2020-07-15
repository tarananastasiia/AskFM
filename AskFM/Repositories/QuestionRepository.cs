using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AskFM.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationContext _context;
        public QuestionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(QuestionDto questionDto, string userId, string questionUserId)
        {
            _context.Questions.Add(new Question()
            {
                AnswerUserId = userId,
                QuestionUserId = questionUserId,
                Text = questionDto.Text,
                Answer = questionDto.Answer,
                IsAnonimized = questionDto.IsAnonimized,
            });
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void QuestionModelsInPage(string userId, int pageNumber = 1, int pageSize = 3)
        {
            var models = _context.Questions
                           .Include(x => x.AnswerUser)
                           .Include(x => x.Comments)
                           .Where(x => x.AnswerUserId == userId && x.Answer != null)
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize).ToList();
        }

        public UserPageDTO PageDTOs(string userId, string questionName)
        {
            int pageSize = 3;
            int pageNumber = 1;
            var dto = new UserPageDTO();
            dto.QuestionsCount = _context.Questions.Count(x => x.AnswerUserId == userId && x.Answer != null);
            dto.PageSize = pageSize;
            dto.User.Id = userId;
            dto.PageNumber = pageNumber;
            dto.Questions = models.Select(question => new QuestionDto()
            {
                Answer = question.Answer,
                Text = question.Text,
                AnswerUserName = question.AnswerUser?.UserName,
                Id = question.Id,
                IsAnonimized = question.IsAnonimized,
                QuestionUserName = questionName,
                Comments = question.Comments.Select(comment => new CommentDto()
                {
                    QuestionId = question.Id,
                    Text = comment.Text,
                    IsAnonimized = comment.IsAnonimized,
                    UserId = comment.IsAnonimized ? null : comment.UserId,
                    UserName = comment.IsAnonimized ? null : comment.UserName,
                }).ToList()
            }).ToList();
        }
    }
}
