using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Web.Helpers;

namespace Web.Models
{
    public class Cache : ICache
    {
        public Cache(IConfigurationRoot _configuration, IHostingEnvironment env, DB db)
        {
            Config = _configuration;
            CSSHash = Math.Abs(File.ReadAllText(Path.Combine(env.WebRootPath, "index.css")).GetHashCode());
            TitleImage = File.ReadAllText(Path.Combine(env.WebRootPath, "title.svg")).CleanSVG().Minify();
            TitleImageXS = File.ReadAllText(Path.Combine(env.WebRootPath, "title-xs.svg")).CleanSVG().Minify();
            Refresh();
        }

        public IConfigurationRoot Config { get; set; }
        public int CSSHash { get; }
        public string TitleImage { get; }
        public string TitleImageXS { get; }

        private IList<Page> pages;
        public IList<Page> Pages => pages ?? SetPages(new DB().Pages.ToList());
        private IList<Page> SetPages(IList<Page> content) => pages = content;

        private string intro;
        public string Intro => intro ?? SetIntro(Pages.First(p => p.Category == "Main" && p.Title == "Intro").Body);
        private string SetIntro(string content) => intro = content;

        private string availabilityMessage;
        public string AvailabilityMessage => availabilityMessage ?? SetAvailability(Pages.First(p => p.Category == "Main" && p.Title == "Availability"));
        private string SetAvailability(Page page) => page != null && page.Description == "On"
            ? (availabilityMessage = page.Body)
            : (availabilityMessage = null);

        public void Refresh()
        {
            SetPages(null);
            SetIntro(null);
            SetAvailability(null);
            var a = Pages;
            var b = Intro;
            var c = AvailabilityMessage;
        }
    }
}
