using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Category
    {
        public Category()
        {
            this.Announcement_Category = new HashSet<Announcement_Category>();
        }

        [Key]
        [Display(Name = "Id Kategorii:")]
        public int Id { get; set; }

        [Display(Name = "Nazwa Kategorii:")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Id rodzica:")]
        [Required]
        public int ParentId { get; set; }

        #region SEO

        [Display(Name = "Tytuł w Google:")]
        [MaxLength(72)]
        public string MetaTitle { get; set; }

        [Display(Name = "Opis strony w Google:")]
        [MaxLength(160)]
        public string MetaDescription { get; set; }

        [Display(Name = "Słowa kluczone w Google:")]
        [MaxLength(160)]
        public string MetaWords { get; set; }

        [Display(Name = "Treść strony:")]
        [MaxLength(500)]
        public string Content { get; set; }

        #endregion

        public virtual ICollection<Announcement_Category> Announcement_Category { get; set; }
    }
}