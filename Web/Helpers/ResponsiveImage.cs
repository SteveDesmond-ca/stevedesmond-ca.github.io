using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Web.Models;

namespace Web.Helpers
{
    [HtmlTargetElement("rimg")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class ResponsiveImage : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;

            var img = new XElement("img", context.AllAttributes.Select(a => new XAttribute(a.Name, a.Value)));
            UpdateTag(img);
            output.Attributes.RemoveAll("base");
            output.Attributes.SetAttribute("src", img.Attribute("src").Value);
            output.Attributes.SetAttribute("srcset", img.Attribute("srcset").Value);
            output.Attributes.SetAttribute("sizes", img.Attribute("sizes").Value);
            output.Attributes.SetAttribute("title", context.AllAttributes["alt"].Value.ToString());
        }

        public static void UpdateTag(XElement image)
        {
            image.Name = "img";

            var imgSrc = image.Attribute("base");
            if (!imgSrc.Value.StartsWith(Settings.ImageBase))
            {
                imgSrc.Value = Settings.ImageBase + "/" + imgSrc.Value;
            }

            var img = Path.GetFileNameWithoutExtension(imgSrc.Value).Split('-')[0];

            if (image.Attribute("src") == null)
                image.Add(new XAttribute("src", Settings.ImageBase + "/" + img + "-1280.jpg"));

            if (image.Attribute("srcset") == null)
            {
                var widths = new[] { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };
                var srcset = string.Join(", ",
                    widths.Select(s => Settings.ImageBase + "/" + img + "-" + (s < 1000 ? "0" : "") + s + ".jpg " + s + "w"));

                image.Add(new XAttribute("srcset", srcset));
            }

            if (image.Attribute("sizes") == null)
            {
                const string sizes = "(max-width: 767px) 100vw, (max-width: 991px) 720px, (max-width: 1199px) 940px, 1140px";
                image.Add(new XAttribute("sizes", sizes));
            }
        }
    }
}
