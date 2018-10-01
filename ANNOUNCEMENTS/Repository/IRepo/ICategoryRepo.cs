using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.IRepo
{
    public interface ICategoryRepo
    {
        IQueryable<Category> GetCategory();
        IQueryable<Announcement> GetAnnouncementsFromCategory(int id);
        string NameForCategory(int id);
    }
}