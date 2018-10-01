using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using Repository.IRepo;
using Repository.Models.Views;

namespace ANNOUNCEMENTS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _repo;

        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }
        // GET: Category
        public ActionResult Index()
        {
            var categories = _repo.GetCategory().AsNoTracking();

            return View(categories);
        }
        public ActionResult ShowAnnouncements(int id)
        {
            var announcements = _repo.GetAnnouncementsFromCategory(id);
            AnnouncementsFromCategoryViewModels model = new AnnouncementsFromCategoryViewModels
            {
                Announcements = announcements.ToList(),
                CategoryName = _repo.NameForCategory(id)
            };
            return View(model);
        }
        
    }
}
