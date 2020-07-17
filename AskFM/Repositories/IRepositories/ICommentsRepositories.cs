using AskFM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Repositories.IRepositories
{
    public interface ICommentsRepositories
    {
        void Add(CommentDto commentDto, string userId, string userName);
    }
}
