using AskFM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services.Contracts
{
    public interface ICommentsService
    {
        void CreateComment(CommentDto commentDto);
        string UserPageId(CommentDto commentDto);
    }
}
