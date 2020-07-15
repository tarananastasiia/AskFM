using AskFM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services.Contracts
{
    public interface IQuestionService
    {
        void Add(QuestionDto questionDto, string userId, string questionUserId);
        UserPageDTO UnansweredQuestionsModelsToList( string userId, string userName);
        void Answer(QuestionDto questionDto, int id);
    }
}
