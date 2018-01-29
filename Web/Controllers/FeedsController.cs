using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public sealed class FeedsController : Controller
    {
        private readonly ICache _cache;
        public FeedsController(ICache cache) => _cache = cache;
        public  IActionResult rss() => new ContentResult { Content = _cache.RSS, ContentType = "application/rss+xml" };
        public IActionResult atom() => new ContentResult { Content = _cache.Atom, ContentType = "application/rss+xml" };
    }
}