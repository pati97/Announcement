using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ANNOUNCEMENTS.Models
{
    public class Announcement
    {
        public Announcement()
        {
            this.Announcement_Category = new HashSet<Announcement_Category>();
        }

        [Display(Name = "Id:")]
        public int Id { get; set; }

        [Display(Name = "Treść ogłoszenia:")]
        [MaxLength(500)]
        public string Content { get; set; }

        [Display(Name = "Tytuł ogłoszenia:")]
        [MaxLength(72)]
        public string Title { get; set; }

        [Display(Name = "Data dodania:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfAdd { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Announcement_Category> Announcement_Category { get; set; }

        public virtual User User { get; set; }
    }
}