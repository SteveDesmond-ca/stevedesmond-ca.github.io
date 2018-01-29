using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Web.Helpers;

namespace Web.Models
{
    public class Cache : ICache
    {
        public IConfigurationRoot Config { get; set; }
        private readonly Func<DB> _newDB;

        public Cache(IConfigurationRoot config, IHostingEnvironment env, Func<DB> dbFac)
        {
            CSSHash = Math.Abs(File.ReadAllText(Path.Combine(env.WebRootPath, "index.css")).GetHashCode());
            TitleImage = File.ReadAllText(Path.Combine(env.WebRootPath, "title.svg")).CleanSVG().Minify();
            TitleImageXS = File.ReadAllText(Path.Combine(env.WebRootPath, "title-xs.svg")).CleanSVG().Minify();

            _newDB = dbFac;
            Config = config;

            #pragma warning disable 4014
            Refresh();
            #pragma warning restore 4014
        }

        public int CSSHash { get; }
        public string TitleImage { get; }
        public string TitleImageXS { get; }
        public IList<Page> Pages { get; private set; }
        public string Intro { get; private set; }
        public string AvailabilityMessage { get; private set; }
        public string RSS { get; private set; }
        public string Atom { get; private set; }

        public async Task Refresh()
        {
            Pages = _newDB().Pages.ToList();
            Intro = Pages.First(p => p.Category == "Main" && p.Title == "Intro")?.Body;
            AvailabilityMessage = Pages.FirstOrDefault(p => p.Category == "Main" && p.Title == "Availability" && p.Description == "On")?.Body;

            var liveBlogPosts = Pages.Where(p => p.Category == "Blog" && p.Crawl).OrderByDescending(p => p.Timestamp).ToList();
            RSS = await liveBlogPosts.ToRSS();
            Atom = await liveBlogPosts.ToAtom();
        }
    }
}
