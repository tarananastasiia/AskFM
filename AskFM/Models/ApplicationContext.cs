using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskFM.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ImageMetaData> ImagesMetaData { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
