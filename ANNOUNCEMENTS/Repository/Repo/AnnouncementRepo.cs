using Repository.IRepo;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repository.Repo
{
    public class AnnouncementRepo : IAnnouncementRepo
    {
        private readonly IAnnouncementContext _db;

        public AnnouncementRepo(IAnnouncementContext db)
        {
            _db = db;
        }

        public IQueryable<Announcement> GetAnnouncement()
        {
            //_db.Database.Log = message => Trace.WriteLine(message);
            var announcements =  _db.Announcements.AsNoTracking();
            return announcements;
        }

        public IQueryable<Announcement> GetPage(int? page = 1, int? pageSize = 10 )
        {
            var announcements = _db.Announcements.OrderByDescending(o => o.DateOfAdd)
                                .Skip((page.Value - 1)*pageSize.Value).Take(pageSize.Value);
            return announcements;
        }

        public Announcement GetAnnouncementById(int id)
        {
            var announcement = _db.Announcements.Find(id);
            return announcement;
        }

        private void DeleteConnectionAnnouncementCategory(int announcementId)
        {
            var list = _db.Announcement_Category.Where(o => o.AnnouncementId == announcementId);

            foreach(var el in list)
            {
                _db.Announcement_Category.Remove(el);
            }
        }

        public void DeleteAnnouncement(int id)
        {
            DeleteConnectionAnnouncementCategory(id);
            Announcement announcement = _db.Announcements.Find(id);
            _db.Announcements.Remove(announcement);
        }

        public void Add(Announcement announcement)
        {
            _db.Announcements.Add(announcement);
        }

        public void Edit(Announcement announcement)
        {
            _db.Entry(announcement).State = EntityState.Modified;
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}