using AskFM.Models;
using AskFM.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace AskFM
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>().ForMember(x => x.UserId, opt => opt.MapFrom(x => x.IsAnonimized ? null : x.UserId)).
                ForMember(x => x.UserName, opt => opt.MapFrom(x => x.IsAnonimized ? null : x.UserComment.Email));
            CreateMap<CommentDto, Comment>();

            CreateMap<Question, QuestionDto>().ForMember(x => x.QuestionUserName, opt => opt.MapFrom(x => x.IsAnonimized ? null : x.QuestionUser.Email));
            CreateMap<QuestionDto, Question>();
        }
    }
}
