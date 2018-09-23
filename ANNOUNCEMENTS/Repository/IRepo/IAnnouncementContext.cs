using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Repository.IRepo
{
    public interface IAnnouncementContext 
    {
        DbSet<Announcement> Announcements { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<User> User { get; set; }
        DbSet<Announcement_Category> Announcement_Category { get; set; }

        DbEntityEntry Entry(object entity);
        int SaveChanges();
        Database Database { get; }

    }
}