using AskFM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services.Contracts
{
    public interface ICommentsService
    {
        void NewComment(CommentDto commentDto, string userId, string userName);
        string UserPageId(CommentDto commentDto);
    }
}
