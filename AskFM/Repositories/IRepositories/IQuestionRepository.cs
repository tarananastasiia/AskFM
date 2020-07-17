using AskFM.Models;
using AskFM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Repositories.IRepositories
{
    public interface IQuestionRepository
    {
        void Add(QuestionDto questionDto, string userId, string questionUserId);
        int QuestionCount(string userId);
        void Answer(QuestionDto questionDto, int id);
        List<Question> UnansweredQuestionsModels(string userId);
        List<Question> PageModel(string userId, int pageNumber = 1, int pageSize = 3);
    }
}
