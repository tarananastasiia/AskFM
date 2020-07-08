using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Models
{
    public class Image
    {

        public int ImageId { get; set; }
        public string Path { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
    }
}
