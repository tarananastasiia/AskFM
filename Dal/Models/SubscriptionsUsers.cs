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
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
        [Key]
        public int SubscriptionId { get; set; }
        public User Subscription { get; set; }
    }
}
