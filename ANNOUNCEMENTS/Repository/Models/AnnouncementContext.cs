using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repository.IRepo;

namespace Repository.Models
{
    
    public class AnnouncementContext : IdentityDbContext, IAnnouncementContext
    {
        public AnnouncementContext()
            : base("DefaultConnection")
        {
            //this.Configuration.ProxyCreationEnabled = false;
        }

        public static AnnouncementContext Create()
        {
            return new AnnouncementContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Announcement_Category> Announcement_Category { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Announcement>().HasRequired
                (x => x.User).WithMany(x => x.Announcement)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}