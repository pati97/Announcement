using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models.Views
{
    public class AnnouncementsFromCategoryViewModels
    {
        public IList<Announcement> Announcements { get; set; }
        public string CategoryName { get; set; }
    }
}