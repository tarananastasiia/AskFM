using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public void Add(QuestionDto questionDto, string userId, string questionUserId)
        {
            questionDto.AnswerUserId = userId;
            questionDto.QuestionUserId = questionUserId;
            Question question = _mapper.Map<Question>(questionDto);

            _questionRepository.Add(question, userId, questionUserId);
        }

        public UserPageDTO UnansweredQuestionsDto(QuestionDto questionDto)
        {
            var dto = new UserPageDTO();
            dto.Questions = _questionRepository.UnansweredQuestionsModels(questionDto.QuestionUserId).
                Select(x => new QuestionDto()
                {
                    QuestionUserId = x.QuestionUserId,
                    Text = x.Text,
                    AnswerUserId = x.AnswerUserId,
                    Id = x.Id,
                    IsAnonimized = x.IsAnonimized,
                    QuestionUserName = x.IsAnonimized ? null : questionDto.QuestionUserName,
                }).ToList();
            return dto;
        }
        public void Answer(Question question, int id)
        {
            _questionRepository.Answer(question, id);
        }
        public UserPageDTO PageDTO(string userId, string questionName, int pageNumber = 1, int pageSize = 3)
        {
            var dto = new UserPageDTO();
            dto.QuestionsCount = _questionRepository.QuestionCount(userId);
            dto.PageSize = pageSize;
            dto.User.Id = userId;
            dto.PageNumber = pageNumber;
            dto.Questions = _questionRepository.PageModel(userId, pageNumber, pageSize).
                Select(question => new QuestionDto()
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
