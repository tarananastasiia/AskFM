using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.ViewModels;
using Dal.Models;
using Dal.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AskFM.Controllers
{
    [Route("subscriptions")]
    public class SubscriptionsController : Controller
    {
        ISubscriptionsRepository _subscriptionsRepository;
        public SubscriptionsController(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }
        [HttpGet("{userId}")]
        public IActionResult Add(string userId)
        {
            var whoSignedUpId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            _subscriptionsRepository.Add(userId,whoSignedUpId);
            return LocalRedirect($"~/question/page?userId={userId}");
        }

    }
}
