using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftJSExporter
{
    public class Element
    {
        private string Type { get; }

        private Dictionary<string, string> Attributes { get; }

        private List<Element> Children { get; }
        
        public Element(string type, Dictionary<string, string> attr)
        {
            Type = type;
            Attributes = attr;
            Children = new List<Element>();
        }

        public void AppendChild(Element child)
        {
            Children.Add(child);
        }

        public string Render()
        {
            var sb = new StringBuilder();
            RenderElement(this, sb, 0);
            return sb.ToString();
        }

        private static void RenderElement(Element element, StringBuilder sb, int level)
        {
            TagBuilder.AddOpeningTag(sb, element.Type, element.Attributes, level);
            
            foreach (var child in element.Children)
            {
                RenderElement(child, sb, level + 1);
            }
            
            TagBuilder.AddClosingTag(sb, element.Type, level);
        }
    }
}