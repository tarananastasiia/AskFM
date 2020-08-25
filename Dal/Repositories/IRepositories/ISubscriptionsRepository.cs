using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repositories.IRepositories
{
    public interface ISubscriptionsRepository
    {
        void Add(string userId, string whoSignedUpId);
    }
}
