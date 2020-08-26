using AskFM.Models;
using AskFM.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskFM.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly ApplicationContext _context;
        public SubscriptionsRepository (ApplicationContext context)
        {
            _context = context;
        }

        public void Add(string userId,string whoSignedUpId)
        {
            var user = _context.Users.Find(whoSignedUpId);

            user.Subscriptions.Add(new SubscriptionsUsers { WhoSignedUpId = user.Id, FollowerId = userId });
            _context.SaveChanges();
        }

        public List<SubscriptionsUsers> MyFollowers(string userId)
        {
            var followers = _context.Subscriptions
               .Include(x => x.Followers)
               .Include(x=>x.User)
               .Where(x => x.WhoSignedUpId == userId).ToList();

            return followers;
        }

        public List<SubscriptionsUsers> DeleteFollowers(string userId)
        {

        }
    }
}
