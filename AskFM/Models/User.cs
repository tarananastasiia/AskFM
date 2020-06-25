using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Models
{
    //Pa$$w0rd
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public FileModel FileModel { get; set; }
    }
}
