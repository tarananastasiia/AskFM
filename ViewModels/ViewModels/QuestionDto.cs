using AskFM.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.ViewModels
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionUserId { get; set; }
        public string AnswerUserId { get; set; }
        public string AnswerUserName { get; set; }
        public string QuestionUserName { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public bool IsAnonimized { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<SubscriptionsUsers> Subscriptions { get; set; }
        public List<SubscriptionsUsers> Followers { get; set; }
        public List<Like> Likes { get; set; }
    }
}
