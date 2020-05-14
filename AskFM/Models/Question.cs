using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionUserId { get; set; }

        public User QuestionUser { get; set; }

        [Required]
        public string AnswerUserId { get; set; }

        public User AnswerUser { get; set; }

        public string Text { get; set; }
        public string Answer { get; set; }
        public bool IsAnonimized { get; set; }
    }
}
