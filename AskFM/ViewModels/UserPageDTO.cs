﻿using AskFM.Models;
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
        public FileModel FileModel { get; set; }
        public UserPageDTO()
        {
            FileModel = new FileModel();
            User = new UserDto();
            Questions = new List<QuestionDto>();
        }
    }
}
