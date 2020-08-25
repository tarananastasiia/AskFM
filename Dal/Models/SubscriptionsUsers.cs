using AskFM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dal.Models
{
    public class SubscriptionsUsers
    {
        public string WhoSignedUpId { get; set; }
        public User User { get; set; }
        public User Followers { get; set; }
        public string FollowerId { get; set; }
    }
}
