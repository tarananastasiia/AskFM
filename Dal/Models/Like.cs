using AskFM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Models
{
    public class Like
    {
        public int QuestionId { get; set; }
        public User User { get; set; }
        public Question Question { get; set;}
        public string UserId { get; set; }
    }
}
