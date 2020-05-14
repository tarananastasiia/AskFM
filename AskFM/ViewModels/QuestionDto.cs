using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.ViewModels
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int? QuestionUserId { get; set; }
        public int AnswerUserId { get; set; }

        public string AnswerUserName { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public bool IsAnonimized { get; set; }
    }
}
