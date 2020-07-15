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
        void Save();
        void QuestionModelsInPage(string userId, int pageNumber = 1, int pageSize = 3);
        UserPageDTO PageDTOs(string userId, string questionName);
    }
}
