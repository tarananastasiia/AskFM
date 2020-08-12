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
        //public DbSet<SubscriptionsUsers> Subscriptions { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SubscriptionsUsers>().HasKey(u => new { u.UserId, u.SubscriptionId });
        //    modelBuilder.Entity<SubscriptionsUsers>().
        //        HasOne(u => u.User).WithMany(u => u.UserSubscriptions).HasForeignKey(u => u.UserId);
        //    modelBuilder.Entity<SubscriptionsUsers>().
        //        HasOne(u => u.Subscription).WithMany(u => u.WhoAddMe).HasForeignKey(u => u.UserId);
        //}
        public DbSet<ImageMetaData> ImagesMetaData { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
