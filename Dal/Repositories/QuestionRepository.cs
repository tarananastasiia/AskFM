using AskFM.Models;
using AskFM.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
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

        public void Add(Question question, string userId, string questionUserId)
        {
            _context.Questions.Add(question);
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

        public void Answer(Question question, int id)
        {
            var questions = _context.Questions.First(x => x.Id == id);
            question.Answer = question.Answer;
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
