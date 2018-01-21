using System.Linq;
using System.Xml.Linq;

namespace Web.Helpers
{
    public static class SVGExtensions
    {
        public static string CleanSVG(this string original)
        {
            var xml = XDocument.Parse(original).Root;
            if (xml == null)
                return null;

            xml.Attributes().Where(AttributeShouldBeRemoved).Remove();
            xml.Descendants().Where(ElementShouldBeRemoved).Remove();
            foreach (var style in xml.Descendants().Attributes().Where(a => a.Name.LocalName == "style"))
                style.Value = "fill:#fff";

            return xml.ToString();
        }

        private static bool ElementShouldBeRemoved(XElement e)
        {
            return e.Name.NamespaceName.Contains("sodipodi")
                || e.Name.LocalName == "defs"
                || e.Name.LocalName == "metadata";
        }

        private static bool AttributeShouldBeRemoved(XAttribute a)
        {
            return a.Name.NamespaceName.Contains("xmlns")
                || a.Name.NamespaceName.Contains("sodipodi")
                || a.Name.NamespaceName.Contains("inkscape")
                || a.Name.LocalName.Contains("sodipodi")
                || a.Name.LocalName.Contains("inkscape")
                || a.Name == "width"
                || a.Name == "height";
        }
    }
}