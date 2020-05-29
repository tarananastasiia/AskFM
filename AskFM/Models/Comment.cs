using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set;}
    }
}
