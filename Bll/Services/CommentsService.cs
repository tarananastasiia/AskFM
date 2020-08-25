using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services
{
    public class CommentsService: ICommentsService
    {
        private readonly ApplicationContext _context;
        ICommentsRepository _commentsRepositories;
        private readonly IMapper _mapper;

        public CommentsService(ApplicationContext context, ICommentsRepository commentsRepositories,
            IMapper mapper)
        {
            _commentsRepositories = commentsRepositories;
            _context = context;
            _mapper = mapper;
        }

        public void CreateComment(CommentDto commentDto)
        {
            Comment comment = _mapper.Map<Comment>(commentDto);
          
            _commentsRepositories.Add(comment);
        }
        public string UserPageId(CommentDto commentDto)
        {
            string pageUserId = _context.Questions.Find(commentDto.QuestionId).AnswerUserId;
            return pageUserId;
        }
    }
}
