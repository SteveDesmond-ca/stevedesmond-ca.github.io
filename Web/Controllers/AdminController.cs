using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public sealed class AdminController : Controller
    {
        private readonly DB _db;
        private readonly ICache _cache;

        public AdminController(DB db, ICache cache)
        {
            _db = db;
            _cache = cache;
        }

        private bool IsAuthorized
        {
            get
            {
                var auth = Request.Headers["Authorization"];
                if (auth.Count != 1)
                    return false;

                var hash = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Convert.FromBase64String(auth.First().Substring(6))));
                return hash == _cache.Config["AdminKey"];
            }
        }

        private IActionResult AuthorizationPrompt
        {
            get
            {
                Response.StatusCode = 401;
                Response.Headers.Add("WWW-Authenticate", "Basic");
                return new ContentResult();
            }
        }

        public IActionResult Index()
        {
            if (!IsAuthorized)
                return AuthorizationPrompt;

            var pages = _cache.Pages.OrderBy(p => p.Category).ThenByDescending(p => p.Timestamp);
            ViewBag.Subtitle = "Admin";
            return View(pages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAuthorized)
                return AuthorizationPrompt;

            var page = new Page { ID = _db.Pages.Max(p => p.ID) + 1 };
            ViewBag.Subtitle = "Create";
            return View("Edit", page);
        }

        [HttpPost]
        public IActionResult Create(Page page)
        {
            if (!IsAuthorized)
                return AuthorizationPrompt;

            if (!ModelState.IsValid)
            {
                ViewBag.Subtitle = "Create";
                return View("Edit", page);
            }

            _db.Pages.Add(page);
            _db.SaveChanges();
            _cache.Refresh();

            ViewBag.Success = true;
            return RedirectToAction("Edit", new { id = page.ID });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAuthorized)
                return AuthorizationPrompt;

            var page = _db.Pages.First(p => p.ID == id);
            ViewBag.Subtitle = "Edit – " + page.Title;
            return View(page);
        }

        [HttpPost]
        public IActionResult Edit(Page page)
        {
            if (!IsAuthorized)
                return AuthorizationPrompt;

            ViewBag.Subtitle = "Edit – " + page.Title;

            if (!ModelState.IsValid)
                return View(page);

            _db.Pages.Update(page);
            _db.SaveChanges();
            _cache.Refresh();

            ViewBag.Success = true;
            return View(page);
        }

        public IActionResult Flush()
        {
            _cache.Refresh();
            return RedirectToAction("Index");
        }
    }
}