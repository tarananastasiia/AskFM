using AskFM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Repositories.IRepositories
{
    public interface IQuestionRepository
    {
        void Add(Question question, string userId, string questionUserId);
        int QuestionCount(string userId);
        List<SubscriptionsUsers> Followers(string userId);
        string UserName(string userId);
        void Answer(Question question, int id);
        List<Question> UnansweredQuestionsModels(string userId);
        List<Question> PageModel(string userId, int pageNumber = 1, int pageSize = 3);
    }
}
