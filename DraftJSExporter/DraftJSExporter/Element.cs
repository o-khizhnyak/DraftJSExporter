using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftJSExporter
{
    public class Element
    {
        private string Type { get; set; }

        private Dictionary<string, string> Attributes { get; set; }

        private Element[] Children { get; set; }

        public Element(string type, Dictionary<string, string> attr)
        {
            Type = type;
            Attributes = attr;
        }

        public void AppendChild(Element child)
        {
            Children.Append(child);
        }

        public string Render()
        {
            return RenderElement(this);
        }

        private static string RenderElement(Element element)
        {
            var open = TagBuilder.CreateOpeningTag(element.Type, element.Attributes);
            var close = TagBuilder.CrateClosingTag(element.Type);
            var children = "";
            return new StringBuilder(open).Append(close).ToString();
        }
    }
}