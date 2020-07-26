using AskFM.Models;
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
        public ImageMetaData Image { get; set; }
        public UserPageDTO()
        {
            Image = new ImageMetaData();
            User = new UserDto();
            Questions = new List<QuestionDto>();
        }
    }
}
