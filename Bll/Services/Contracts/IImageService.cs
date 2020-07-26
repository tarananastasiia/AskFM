using AskFM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace AskFM.Services
{
    public interface IImageService
    {
        void Save(Users user, FormFileDto uploadedFile);
        byte[] FileStorageRead(string path);

        byte[] GetContent(string userId);
    }
}
