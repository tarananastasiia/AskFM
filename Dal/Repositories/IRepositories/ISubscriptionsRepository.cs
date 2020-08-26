using AskFM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskFM.Repositories.IRepositories
{
    public interface ISubscriptionsRepository
    {
        void Add(string userId, string whoSignedUpId);
        List<SubscriptionsUsers> MyFollowers(string userId);
        List<SubscriptionsUsers> DeleteFollowers(string userId);
    }
}
