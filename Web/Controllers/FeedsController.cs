using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using Web.Models;

namespace Web.Controllers
{
    public sealed class FeedsController : Controller
    {
        private readonly IEnumerable<Page> livePosts;

        public FeedsController(ICache cache)
        {
            livePosts = cache.Pages.Where(p => p.Category == "Blog" && p.Crawl).OrderByDescending(p => p.Timestamp);
        }

        public async Task<IActionResult> rss()
        {
            var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Async = true, Indent = true, Encoding = Encoding.UTF8, WriteEndDocumentOnClose = true }))
            {
                var rssWriter = new RssFeedWriter(xmlWriter);
                var tasks = new List<Task>
                {
                    rssWriter.WriteTitle(Settings.Title),
                    rssWriter.WriteDescription(Settings.Description),
                    rssWriter.Write(new SyndicationLink(new Uri(Settings.Domain)))
                };
                tasks.AddRange(livePosts.Select(post => rssWriter.Write(SyndicationItem(post))));
                Task.WaitAll(tasks.ToArray());
                await rssWriter.Flush();
            }

            return new ContentResult { Content = stringWriter.ToString(), ContentType = "application/rss+xml" };
        }

        public async Task<IActionResult> atom()
        {
            var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Async = true, Indent = true, Encoding = Encoding.UTF8, WriteEndDocumentOnClose = true }))
            {
                var atomWriter = new AtomFeedWriter(xmlWriter);
                var tasks = new List<Task>
                {
                    atomWriter.WriteTitle(Settings.Title),
                    atomWriter.WriteSubtitle(Settings.Description),
                    atomWriter.WriteId(Settings.Domain),
                    atomWriter.Write(new SyndicationLink(new Uri(Settings.Domain))),
                    atomWriter.WriteUpdated(livePosts.First().Timestamp.ToLocalTime())
                };
                tasks.AddRange(livePosts.Select(post => atomWriter.Write(SyndicationItem(post))));
                Task.WaitAll(tasks.ToArray());
                await atomWriter.Flush();
            }
            
            return new ContentResult { Content = stringWriter.ToString(), ContentType = "application/rss+xml" };
        }

        private static SyndicationItem SyndicationItem(Page p)
        {
            var post = new SyndicationItem
            {
                Id = p.FullURL,
                Title = p.Title,
                Description = p.Description,
                Published = p.Timestamp.ToLocalTime(),
                LastUpdated = p.Timestamp.ToLocalTime()
            };
            post.AddLink(new SyndicationLink(new Uri(p.FullURL)));
            post.AddContributor(new SyndicationPerson("Steve Desmond", "steve@stevedesmond.ca"));
            return post;
        }
    }
}