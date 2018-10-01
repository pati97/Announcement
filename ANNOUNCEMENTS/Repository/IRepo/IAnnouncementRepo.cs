using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Repository.IRepo
{
    public interface IAnnouncementRepo
    { 
        IQueryable<Announcement> GetAnnouncement();
        IQueryable<Announcement> GetPage(int? page, int? pageSize);
        Announcement GetAnnouncementById(int id);
        
        void Add(Announcement announcement);
        void Edit(Announcement announcement);
        void DeleteAnnouncement(int id);
        void SaveChanges();
    }
}