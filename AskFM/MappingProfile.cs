using AskFM.Models;
using AskFM.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();

            CreateMap<Question, QuestionDto>().ForMember(x => x.QuestionUserName, opt => opt.MapFrom(x => x.IsAnonimized ? null : x.QuestionUser.Email));
            CreateMap<QuestionDto, Question>();
        }
    }
}
