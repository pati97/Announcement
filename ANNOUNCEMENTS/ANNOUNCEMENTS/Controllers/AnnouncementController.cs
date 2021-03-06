﻿using System;
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
using PagedList;

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
        public ActionResult Index(int? page, string sortOrder, string searchData, string FilterValue)
        {
            int currentPage = page ?? 1;
            int onPage = 4;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "IdAscending" : "";
            ViewBag.DateOfAddSort = sortOrder == "DataOfAddDescending" ? "DataOfAddAscending" : "DataOfAddDescending";
            ViewBag.TitleSort = sortOrder == "TitleAscending" ? "TitleDescending" : "TitleAscending";
            ViewBag.ContentSort = sortOrder == "ContentAscending" ? "ContentDescending" : "ContentAscending";

            if (searchData != null)
            {
                page = 1;
            }
            else
            {
                searchData = FilterValue;
            }
            ViewBag.FilterValue = searchData;

            var announcements = _repo.GetAnnouncement();

            if (!String.IsNullOrEmpty(searchData))
            {
                announcements = announcements.Where(o => o.Content.ToUpper().Contains(searchData.ToUpper())
                                || o.Title.ToUpper().Contains(searchData.ToUpper()));
            }

            switch (sortOrder)
            {
                case "DataOfAddDescending":
                    announcements = announcements.OrderByDescending(s => s.DateOfAdd);
                    break;
                case "DataOfAddAscending":
                    announcements = announcements.OrderBy(s => s.DateOfAdd);
                    break;
                case "TitleAscending":
                    announcements = announcements.OrderBy(s => s.Title);
                    break;
                case "TitleDescending":
                    announcements = announcements.OrderByDescending(s => s.Title);
                    break;
                case "ContentAscending":
                    announcements = announcements.OrderBy(s => s.Content);
                    break;
                case "ContentDescending":
                    announcements = announcements.OrderByDescending(s => s.Content);
                    break;
                case "IdAscending":
                    announcements = announcements.OrderBy(s => s.Id);
                    break;
                default:
                    announcements = announcements.OrderByDescending(s => s.Id);
                    break;
            }

            return View(announcements.ToPagedList(currentPage, onPage));

        }

        [OutputCache(Duration = 1000)]
        public ActionResult MyAnnouncements(int? page)
        {
            int currentpage = page ?? 1;
            int onPage = 3;
            string userId = User.Identity.GetUserId();
            var announcements = _repo.GetAnnouncement();
            announcements = announcements.OrderByDescending(d => d.DateOfAdd).Where(o => o.UserId == userId);

            return View(announcements.ToPagedList(currentpage, onPage));
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
                    return RedirectToAction("MyAnnouncements");
                }
                catch
                {
                    return View(announcement);
                }
            }
            return View(announcement);
        }

        // GET: Announcement/Edit/5
        [Authorize]
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
            else if (announcement.UserId != User.Identity.GetUserId() && !(User.IsInRole("Admin") || User.IsInRole("Pracownik")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(announcement);
        }

        // POST: Announcement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
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
        [Authorize]
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
            else if (announcement.UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        public ActionResult Partial(int? page)
        {
            int currentPage = page ?? 1;
            int onPage = 4;
            var announcements = _repo.GetAnnouncement();
            announcements = announcements.OrderByDescending(o => o.DateOfAdd);
            return PartialView("Index", announcements.ToPagedList<Announcement>(currentPage, onPage));
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
