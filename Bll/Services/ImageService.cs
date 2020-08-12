using AskFM.Models;
using AskFM.Repositories.IRepositories;
using AskFM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace AskFM.Services
{
    public class ImageService : IImageService
    {
        private readonly IFileStorageService _imageStorageService;
        private readonly IImageMetaDataRepository _imageMetaDataRepository;
        public ImageService(IFileStorageService imageStorageService,
            IImageMetaDataRepository imageMetaDataRepository)
        {
            _imageMetaDataRepository = imageMetaDataRepository;
            _imageStorageService = imageStorageService;
        }

        public void Save(User user, FormFileDto uploadedFile)
        {
            if (uploadedFile.Length > 0)
            {
                var imageId = Guid.NewGuid();
                string path = null;

                if (uploadedFile != null)
                    path = "/Image/" + imageId + Path.GetExtension(uploadedFile.FileName);

                using (var ms = new MemoryStream())
                {
                    var fileBytes = uploadedFile.ContentStream.ToArray();
                    _imageStorageService.Save(fileBytes, path);
                }
                ImageMetaData image = new ImageMetaData()
                {
                    UserId = user.Id,
                    Path = path,
                };
                _imageMetaDataRepository.Save(image);
            }
        }
        public byte[] FileStorageRead(string path)
        {
            return _imageStorageService.Read(path);
        }

        public byte[] GetContent(string userId)
        {
            return FileStorageRead(_imageMetaDataRepository.Get(x => x.UserId == userId).Path);
        }

    }
}
