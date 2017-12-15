using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Web.Models
{
    public static class Cache
    {
        public static IConfigurationRoot Config { get; set; }
        public static int CSSHash { get; internal set; }
        public static string TitleImage { get; internal set; }
        public static string TitleImageXS { get; internal set; }

        private static IEnumerable<Page> pages;
        public static IEnumerable<Page> Pages => pages ?? setPages(new DB().Pages.ToList());

        private static IEnumerable<Page> setPages(IEnumerable<Page> content)
        {
            return pages = content;
        }

        private static string intro;
        public static string Intro => intro ?? setIntro(Pages.First(p => p.Category == "Main" && p.Title == "Intro").Body);

        private static string availabilityMessage;
        public static string AvailabilityMessage => availabilityMessage ?? setAvailability(Pages.First(p => p.Category == "Main" && p.Title == "Availability"));

        private static string setIntro(string content)
        {
            return intro = content;
        }

        private static string setAvailability(Page page)
        {
            if (page != null && page.Description == "On")
                return availabilityMessage = page.Body;

            return availabilityMessage = null;
        }

        public static void Reset()
        {
            setPages(null);
            setIntro(null);
            setAvailability(null);
            var a = Pages;
            var b = Intro;
            var c = AvailabilityMessage;
        }
    }
}
