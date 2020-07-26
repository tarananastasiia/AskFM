using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ViewModels.ViewModels
{
    public class FormFileDto
    {
        public long Length { get; set; }
        public string FileName { get; set; }
        public MemoryStream ContentStream { get; set; }
    }
}
