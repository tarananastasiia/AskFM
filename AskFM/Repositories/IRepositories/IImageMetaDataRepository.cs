using AskFM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Repositories.IRepositories
{
    public interface IImageMetaDataRepository
    {
        void Save(ImageMetaData imageMetaData);
    }
}
