using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Models
{
    //Pa$$w0rd
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public List<ImageMetaData> Images { get; set; }
        public List<SubscriptionsUsers> Subscriptions { get; set; }
        public List<SubscriptionsUsers> Followers { get; set; }
        public User()
        {
            Subscriptions = new List<SubscriptionsUsers>();
            Followers = new List<SubscriptionsUsers>();
        }
    }
}
