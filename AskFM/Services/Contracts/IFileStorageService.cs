using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Services.Contracts
{
    public interface IFileStorageService
    {
        void Save(byte[] content, string path);
        byte[] Read(string path);
    }
}
