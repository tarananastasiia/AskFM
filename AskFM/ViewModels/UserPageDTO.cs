using AskFM.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.ViewModels
{
    public class UserPageDTO
    {
        public List<QuestionDto> Questions { get; set; }
        public UserDto User { get; set; }
        public int QuestionsCount { get; set;}
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public Image Image { get; set; }
        public UserPageDTO()
        {
            Image = new Image();
            User = new UserDto();
            Questions = new List<QuestionDto>();
        }
    }
}
