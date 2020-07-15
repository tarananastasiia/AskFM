using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AskFM.Services
{
    public class QuestionService: IQuestionService
    {
        private readonly ApplicationContext _context;
        IQuestionRepository _questionRepository;

        public QuestionService(ApplicationContext context,
            IQuestionRepository questionRepository )
        {
            _context = context;
            _questionRepository = questionRepository;
        }

        public void Add(QuestionDto questionDto, string userId, string questionUserId)
        {
            _questionRepository.Add(questionDto,userId,questionUserId);
            _questionRepository.Save();
        }

        public UserPageDTO UnansweredQuestionsModelsToList(string userId, string userName)
        {
            var dto = new UserPageDTO();
            var models = _context.Questions
                .Where(x => x.AnswerUserId == userId && x.Answer == null).ToList();
            dto.Questions = models.Select(x => new QuestionDto()
            {
                QuestionUserId = x.QuestionUserId,
                Text = x.Text,
                AnswerUserId = x.AnswerUserId,
                Id = x.Id,
                IsAnonimized = x.IsAnonimized,
                QuestionUserName = userName,
            }).ToList();
            return (dto);
        }
        public void Answer(QuestionDto questionDto, int id)
        {
            var question = _context.Questions.First(x => x.Id == id);
            question.Answer = questionDto.Answer;
            _questionRepository.Save();
        }
    }
}
