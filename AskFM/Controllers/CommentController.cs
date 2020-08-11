using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AskFM.Controllers
{
    [Route("comment")]
    public class CommentController : Controller
    {
        ICommentsService _commentsService;

        public CommentController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }
        [HttpPost]//TODO: move id to commentdto
        public IActionResult Comments(CommentDto commentDto)
        {
            commentDto.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _commentsService.CreateComment(commentDto);

            return LocalRedirect($"~/question/page?userId={_commentsService.UserPageId(commentDto)}");
        }
    }
}   