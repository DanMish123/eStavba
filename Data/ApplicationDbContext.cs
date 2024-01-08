using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eStavba.Models;

namespace eStavba.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ForumReplyModel> ForumReplies { get; set; }
        public DbSet<ForumThreadModel> ForumThreads { get; set; }
        public DbSet<Forum>? Forum { get; set; }
        public DbSet<AnnouncementModel>? AnnouncementModel { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<ReportProblemModel> ReportedProblems { get; set; }

    }
}