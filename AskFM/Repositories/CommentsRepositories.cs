using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.ViewModels;
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
        public void Add(CommentDto commentDto, string userId, string userName)
        {
            _context.Comments.Add(new Comment()
            {
                QuestionId = commentDto.QuestionId,
                Text = commentDto.Text,
                IsAnonimized = commentDto.IsAnonimized,
                UserId = userId,
                UserName = userName,
            }); 
            _context.SaveChanges();
        }
    }
}
