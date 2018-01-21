using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public sealed class SitemapController : Controller
    {
        private readonly ICache _cache;

        public SitemapController(ICache cache)
        {
            _cache = cache;
        }

        [HttpGet("sitemap.xml")]
        public string Index()
        {
            var xml = new XElement(XName.Get("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9"));

            xml.Add(urlElementFor(Settings.Domain));
            addCmsSitemap(xml);

            return new XDocument(new XDeclaration("1.0", "UTF-8", null), xml).ToString().Replace(" xmlns=\"\"", "");
        }

        private void addCmsSitemap(XContainer xml)
        {
            var pages = _cache.Pages.GroupBy(p => p.Category);
            foreach (var category in pages)
            {
                var categoryName = category.Key.ToLower();

                if (!categoryName.Matches("Home"))
                    xml.Add(urlElementFor(Settings.Domain + "/" + categoryName));

                foreach (var page in category.Where(p => !p.Category.Matches("Home") && p.Crawl))
                    xml.Add(urlElementFor(page.FullURL));
            }
        }

        private static XElement urlElementFor(string url)
        {
            return new XElement("url", new XElement("loc", url));
        }
    }
}