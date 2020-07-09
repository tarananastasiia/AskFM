﻿using AskFM.Models;
using AskFM.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Repositories
{
    public class ImageMetaDataRepository: IImageMetaDataRepository
    {
        private readonly ApplicationContext _context;
        public ImageMetaDataRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Save(ImageMetaData imageMetaData)
        {
            _context.ImagesMetaData.Add(imageMetaData);
            _context.SaveChanges();
        }
    }
}
