using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using Web.Models;

namespace Web.Helpers
{
    public static class FeedHelpers
    {
        public static async Task<string> ToRSS(this IList<Page> pages)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter,
                    new XmlWriterSettings
                    {
                        Async = true,
                        Indent = true,
                        Encoding = Encoding.UTF8,
                        WriteEndDocumentOnClose = true
                    }))
                {
                    var rssWriter = new RssFeedWriter(xmlWriter);
                    var tasks = new List<Task>
                    {
                        rssWriter.WriteTitle(Settings.Title),
                        rssWriter.WriteDescription(Settings.Description),
                        rssWriter.Write(new SyndicationLink(new Uri(Settings.Domain)))
                    };
                    tasks.AddRange(pages.Select(p => rssWriter.Write(p.ToSyndicationItem())));
                    Task.WaitAll(tasks.ToArray());
                    await rssWriter.Flush();
                }

                return stringWriter.ToString();
            }
        }

        public static async Task<string> ToAtom(this IList<Page> pages)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter,
                    new XmlWriterSettings
                    {
                        Async = true,
                        Indent = true,
                        Encoding = Encoding.UTF8,
                        WriteEndDocumentOnClose = true
                    }))
                {
                    var atomWriter = new AtomFeedWriter(xmlWriter);
                    var tasks = new List<Task>
                    {
                        atomWriter.WriteTitle(Settings.Title),
                        atomWriter.WriteSubtitle(Settings.Description),
                        atomWriter.WriteId(Settings.Domain),
                        atomWriter.Write(new SyndicationLink(new Uri(Settings.Domain))),
                        atomWriter.WriteUpdated(pages.First().Timestamp.ToLocalTime())
                    };
                    tasks.AddRange(pages.Select(p => atomWriter.Write(p.ToSyndicationItem())));
                    Task.WaitAll(tasks.ToArray());
                    await atomWriter.Flush();
                }

                return stringWriter.ToString();
            }
        }

        private static SyndicationItem ToSyndicationItem(this Page p)
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
            post.AddContributor(new SyndicationPerson(Settings.Title, Settings.EmailFromAndTo));
            return post;
        }
    }
}