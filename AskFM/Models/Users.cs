using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Models
{
    //Pa$$w0rd
    public class Users : IdentityUser
    {
        public int Year { get; set; }
        public List<ImageMetaData> Images { get; set; }
    }
}
