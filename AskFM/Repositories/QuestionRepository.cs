﻿using AskFM.Models;
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
        public int QuestionCount(string userId)
        {
            int count = _context.Questions.Count(x => x.AnswerUserId == userId && x.Answer != null);
            return count;
        }

        public List<Question> UnansweredQuestionsModels(string userId)
        {
            var models = _context.Questions
                .Where(x => x.AnswerUserId == userId && x.Answer == null).ToList();
            return models;
        }

        public Question Answer(int id)
        {
            var question = _context.Questions.First(x => x.Id == id);
            return question;
        }
        public List<Question> PageModel(string userId, int pageNumber = 1, int pageSize = 3)
        {
            var user = _context.Users.Find(userId);
            var models = _context.Questions
                           .Include(x => x.AnswerUser)
                           .Include(x => x.Comments)
                           .Where(x => x.AnswerUserId == userId && x.Answer != null)
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize).ToList();
            return models;
        }
    }
}