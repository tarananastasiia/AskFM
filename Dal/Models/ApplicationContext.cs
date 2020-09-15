using Dal.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SubscriptionsUsers>()
                .HasKey(t => new { t.WhoSignedUpId, t.FollowerId });

            modelBuilder.Entity<SubscriptionsUsers>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.Subscriptions)
                .HasForeignKey(sc => sc.WhoSignedUpId).OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<SubscriptionsUsers>()
                .HasOne(sc => sc.Followers)
                .WithMany(c => c.Followers)
                .HasForeignKey(sc => sc.FollowerId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Like>()
                .HasKey(t => new { t.QuestionId, t.UserId });

            modelBuilder.Entity<Like>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.Likes)
                .HasForeignKey(sc => sc.UserId).OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<Like>()
                .HasOne(sc => sc.Question)
                .WithMany(c => c.Likes)
                .HasForeignKey(sc => sc.QuestionId);
        }

        public DbSet<Like> Likes { get; set; }
        public DbSet<SubscriptionsUsers> Subscriptions { get; set; }
        public DbSet<ImageMetaData> ImagesMetaData { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
