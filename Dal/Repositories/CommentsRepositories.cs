using AskFM.Models;
using AskFM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Repositories
{
    public class CommentsRepositories : ICommentsRepositories
    {
        private readonly ApplicationContext _context;

        public CommentsRepositories(ApplicationContext context)
        {
            _context = context;
        }
        public void Add(Comment comment)
        {
            _context.Comments.Add(comment); 
            _context.SaveChanges();
        }
    }
}
