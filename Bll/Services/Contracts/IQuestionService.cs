using AskFM.Models;
using AskFM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services.Contracts
{
    public interface IQuestionService
    {
        void Add(QuestionDto questionDto, string userId, string questionUserId);
        UserPageDTO UnansweredQuestionsDto( string userId, string userName);
        void Answer(Question question, int id);
        UserPageDTO PageDTO(string userId, string questionName, int pageNumber = 1, int pageSize = 3);
    }
}
