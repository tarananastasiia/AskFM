using AskFM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Repositories.IRepositories
{
    public interface ICommentsRepositories
    {
        void Add(Comment comment, string userId, string userName);
    }
}
