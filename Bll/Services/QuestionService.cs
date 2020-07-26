using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AskFM.Services
{
    public class QuestionService : IQuestionService
    {
        IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public void Add(QuestionDto questionDto, string userId, string questionUserId)
        {
            var question = new Question()
            {
                AnswerUserId = userId,
                QuestionUserId = questionUserId,
                Text = questionDto.Text,
                Answer = questionDto.Answer,
                IsAnonimized = questionDto.IsAnonimized,
            };
            _questionRepository.Add(question, userId, questionUserId);
        }

        public UserPageDTO UnansweredQuestionsDto(string userId, string userName)
        {
            var dto = new UserPageDTO();
            dto.Questions = _questionRepository.UnansweredQuestionsModels(userId).Select(x => new QuestionDto()
            {
                QuestionUserId = x.QuestionUserId,
                Text = x.Text,
                AnswerUserId = x.AnswerUserId,
                Id = x.Id,
                IsAnonimized = x.IsAnonimized,
                QuestionUserName = userName,
            }).ToList();
            return dto;
        }
        public void Answer(Question question, int id)
        {
            _questionRepository.Answer(question,id);
        }
        public UserPageDTO PageDTO(string userId, string questionName, int pageNumber = 1, int pageSize = 3)
        {
            var dto = new UserPageDTO();
            dto.QuestionsCount=_questionRepository.QuestionCount(userId);
            dto.PageSize = pageSize;
            dto.User.Id = userId;
            dto.PageNumber = pageNumber;
            dto.Questions = _questionRepository.PageModel(userId, pageNumber, pageSize).Select(question => new QuestionDto()
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
            return dto;
        }
    }
}
