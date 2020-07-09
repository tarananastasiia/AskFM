using AskFM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services
{
    public class FileStorageService : IFileStorageService
    {
        //TODO: Add configured file directory
        public FileStorageService()
        {

        }

        public byte[] Read(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void Save(byte[] content, string path)
        {
            File.WriteAllBytes(path, content);
        }
    }
}
