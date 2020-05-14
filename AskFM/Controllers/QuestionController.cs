using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AskFM.Controllers
{
    [Route ("user-question")]
    public class QuestionController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Post(string _text)
        {
            string authData = _text;
            return Content(authData);
        }
    }
}