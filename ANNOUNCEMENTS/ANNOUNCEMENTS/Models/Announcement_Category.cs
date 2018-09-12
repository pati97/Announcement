using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ANNOUNCEMENTS.Models
{
    public class Announcement_Category
    {
        public Announcement_Category()
        {
        }

        public int Id { get; set; }
        public int AnnouncementId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Announcement Announcement { get; set; }

    }
}