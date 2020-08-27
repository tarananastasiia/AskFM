using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.Repositories;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
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
            var whoSignedUpId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _subscriptionsRepository.Add(userId, whoSignedUpId);
            return LocalRedirect($"~/question/page?userId={userId}");
        }

        [HttpGet("all")]
        public IActionResult AllSubscriptions(string userId)
        {
            var a = new User();
            a.Followers = _subscriptionsRepository.MyFollowers(userId);
            return View("AllSubscription",a);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteFollowers(string userId)
        {
            var whoSignedUpId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _subscriptionsRepository.DeleteFollowers(userId);
            return LocalRedirect($"~/subscriptions/all?userId={whoSignedUpId}");
        }

    }
}
