using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Web.Models;

namespace Web.Helpers
{
    [HtmlTargetElement("content", Attributes = "page, summarize")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public sealed class ContentRenderer : TagHelper
    {
        public Page Page { get; set; }
        public bool Summarize { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "content");
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = XDocument.Parse("<root>" + Page.Body + "</root>");
            if (Summarize)
                content.Root?.ReplaceNodes(content.Root?.Nodes().Take(2));
            
            var links = content.Descendants("a");
            foreach (var link in links)
            {
                var imgSrc = link.Attribute("href");
                if (imgSrc != null && imgSrc.Value.StartsWith("/images"))
                    link.SetAttributeValue("href", Settings.CDN + imgSrc.Value);
            }

            var images = content.Descendants("img");
            foreach (var image in images)
            {
                var imgSrc = image.Attribute("src");
                if (imgSrc != null && imgSrc.Value.StartsWith("/images"))
                    image.SetAttributeValue("src", Settings.CDN + imgSrc.Value);
            }

            var responsiveImages = content.Descendants("rimg");
            foreach (var image in responsiveImages)
                ResponsiveImage.UpdateTag(image);

            // ReSharper disable once MustUseReturnValue
            output.Content.SetHtmlContent(content.Root?.ToString().Replace("<root>", "").Replace("</root>", ""));
        }
    }
}