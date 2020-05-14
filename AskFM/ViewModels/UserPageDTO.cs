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

       public UserPageDTO()
        {
            User = new UserDto();
            Questions = new List<QuestionDto>();
        }

    }
}
