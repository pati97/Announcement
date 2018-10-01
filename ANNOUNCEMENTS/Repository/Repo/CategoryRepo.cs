using Repository.IRepo;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly IAnnouncementContext _db;
        public CategoryRepo(IAnnouncementContext db)
        {
            _db = db;
        }

        public IQueryable<Category> GetCategory()
        {
            var category = _db.Categories.AsNoTracking();
            return category;
        }

        public IQueryable<Announcement> GetAnnouncementsFromCategory(int id)
        {
            var announcements = from o in _db.Announcements
                                join k in _db.Announcement_Category on o.Id equals k.Id
                                where k.CategoryId == id
                                select o;

            return announcements;
        }

        public string NameForCategory(int id)
        {
            var name = _db.Categories.Find(id).Name;
            return name;
        }
    }
}