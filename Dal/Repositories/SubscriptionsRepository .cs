using AskFM.Models;
using Dal.Models;
using Dal.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repositories
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

            user.SubscriptionsUser.Add(new SubscriptionsUsers { WhoSignedUpId = user.Id, FollowerId = userId });
            _context.SaveChanges();
        }
    }
}
