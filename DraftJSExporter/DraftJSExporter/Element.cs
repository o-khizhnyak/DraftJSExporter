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
        
        private string Text { get; }

        public bool Inline { get; }
        
        public Element(string type = null, Dictionary<string, string> attr = null, string text = null, bool inline = false)
        {
            Type = type;
            Attributes = attr ?? new Dictionary<string, string>();
            Text = text;
            Inline = inline;
            Children = new List<Element>();
        }

        public void AppendChild(Element child)
        {
            Children.Add(child);
        }

        public string Render()
        {
            var sb = new StringBuilder();
            RenderElement(this, sb, 0, true);
            return sb.ToString();
        }

        private static void RenderElement(Element element, StringBuilder sb, int level, bool addTab)
        { 
            TagBuilder.AddOpeningTag(sb, element.Type, element.Attributes, level, element.Inline, addTab);
            
            if (element.Text != null)
            {
                TagBuilder.AddText(sb, element.Text, element.Inline, level);                
            }

            var inline = false;
            foreach (var child in element.Children)
            {
                RenderElement(child, sb, level + 1, !(inline && child.Inline));
                inline = child.Inline;
            }

            if (element.Type != null)
            {
                TagBuilder.AddClosingTag(sb, element.Type, level, element.Inline);                
            }
        }
    }
}