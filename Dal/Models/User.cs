﻿using Dal.Models;
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
        public List<SubscriptionsUsers> SubscriptionsUser { get; set; }
        public List<SubscriptionsUsers> UserSubscriptions { get; set; }
        public User()
        {
            SubscriptionsUser = new List<SubscriptionsUsers>();
            UserSubscriptions = new List<SubscriptionsUsers>();
        }
    }
}
