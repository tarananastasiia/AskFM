using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using AskFM.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AskFM.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IWebHostEnvironment _appEnvironment;
        private readonly IFileStorageService _imageStorageService;
        IImageMetaDataRepository _imageMetaDataRepository;


        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IWebHostEnvironment appEnvironment,
            IFileStorageService imageStorageService,
             IImageMetaDataRepository imageMetaDataRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
            _imageStorageService = imageStorageService;
            _imageMetaDataRepository = imageMetaDataRepository;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                var imageId = Guid.NewGuid();
                string path = null;

                if (uploadedFile != null)
                    path = "/Image/" + imageId + Path.GetExtension(uploadedFile.FileName);

                if (uploadedFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        uploadedFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        _imageStorageService.Save(fileBytes, path);
                    }
                }

                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Year = model.Year,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                ImageMetaData image = new ImageMetaData()
                {
                    UserId = user.Id,
                    Path = path,
                };

                _imageMetaDataRepository.Save(image);
                //_context.ImagesMetaData.Add(image);
                //_context.SaveChanges();

                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

       
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}