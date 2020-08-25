using AskFM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Repositories.IRepositories
{
    public interface ICommentsRepository
    {
        void Add(Comment comment);
    }
}
