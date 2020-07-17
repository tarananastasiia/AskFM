using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services
{
    public class CommentsService: ICommentsService
    {
        private readonly ApplicationContext _context;
        ICommentsRepositories _commentsRepositories;

        public CommentsService(ApplicationContext context, ICommentsRepositories commentsRepositories)
        {
            _commentsRepositories = commentsRepositories;
            _context = context;
        }

        public void NewComment(CommentDto commentDto, string userId, string userName)
        {
            _commentsRepositories.Add(commentDto,userId,userName);
        }
        public string UserPageId(CommentDto commentDto)
        {
            string pageUserId = _context.Questions.Find(commentDto.QuestionId).AnswerUserId;
            return pageUserId;
        }
    }
}
