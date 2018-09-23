using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using System.Diagnostics;
using Repository.Repo;
using Repository.IRepo;
using Microsoft.AspNet.Identity;

namespace ANNOUNCEMENTS.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementRepo _repo;

        public AnnouncementController(IAnnouncementRepo repo)
        {
            _repo = repo;
        }
        
        // GET: Announcement
        public ActionResult Index()
        {
            var announcements = _repo.GetAnnouncement();
            return View(announcements);
        }

        // GET: Announcement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = _repo.GetAnnouncementById((int)id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // GET: Announcement/Create
        public ActionResult Create()
        {   
            return View();
        }

        // POST: Announcement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Content,Title")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.UserId = User.Identity.GetUserId();
                announcement.DateOfAdd = DateTime.Now;
                try
                {
                    _repo.Add(announcement);
                    _repo.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(announcement);
                }
            }
            return View(announcement);
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = _repo.GetAnnouncementById((int)id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,Title,DateOfAdd,UserId")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //announcement.UserId = "fdfdfd";
                    _repo.Edit(announcement);
                    _repo.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = true;
                    return View(announcement);
                }
            }
            ViewBag.Error = false;
            return View(announcement);
        }

        // GET: Announcement/Delete/5
        public ActionResult Delete(int? id, bool? error)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = _repo.GetAnnouncementById((int)id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            if (error != null)
                ViewBag.Error = true;

            return View(announcement);
        }

        // POST: Announcement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteAnnouncement(id);
            try
            {
                _repo.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id, error = true });
            }

            return RedirectToAction("Index");
        }

        //Get: /Announcement/
        public ActionResult Partial()
        {
            var announcement = _repo.GetAnnouncement();
            return PartialView("Index", announcement);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
