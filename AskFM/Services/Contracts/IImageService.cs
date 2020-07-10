using AskFM.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services
{
    public interface IImageService
    {
        void Save(User user, IFormFile uploadedFile);
        byte[] FileStorageRead(string path);

        byte[] GetContent(string userId);
    }
}
