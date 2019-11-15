using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftJSExporter
{
    public class HtmlElement
    {
        public string Type { get; }

        public IReadOnlyDictionary<string, string> Attributes { get; }

        public List<HtmlElement> Children { get; }
        
        public string Text { get; set; }

        public bool Inline { get; }
        
        public bool Wrapper { get; }
        
        public bool SelfClosing { get; }
        
        public HtmlElement(string type = null, IReadOnlyDictionary<string, string> attr = null, string text = null, 
            bool inline = false, bool wrapper = false, bool selfClosing = false)
        {
            Type = type;
            Attributes = attr ?? new Dictionary<string, string>();
            Text = text;
            Inline = inline;
            Children = new List<HtmlElement>();
            Wrapper = wrapper;
            SelfClosing = selfClosing;
        }

        public void AppendChild(HtmlElement child)
        {
            if (!SelfClosing)
            {
                Children.Add(child);                
            }
        }
        
        public string Render()
        {
            var sb = new StringBuilder();
            RenderElement(this, sb, 0, true, true, false, true);
            return sb.ToString();
        }

        private static void RenderElement(HtmlElement element, StringBuilder sb, int level, bool addTab, bool lastChild, 
            bool parentIsInline, bool parentIsLastChild)
        {
            TagBuilder.AddOpeningTag(sb, element.Type, element.Attributes, level, element.Inline, addTab, element.SelfClosing);

            if (element.SelfClosing)
            {
                TagBuilder.CloseTag(sb, parentIsLastChild);
            }
            else
            {            
                if (element.Text != null)
                {
                    TagBuilder.AddText(sb, element.Text, element.Inline, level);         
                }

                var inline = false;
                var childrenCount = element.Children.Count;
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = element.Children[i];
                    RenderElement(child, sb, element.Type == null && element.Text == null ? level : level + 1, 
                        !(inline && child.Inline) && !(child.Type == null && !child.Inline) && !element.Inline, 
                        i == childrenCount - 1, element.Inline, lastChild);
                    inline = child.Inline;
                }

                TagBuilder.AddClosingTag(sb, element.Type, level, element.Inline, lastChild, parentIsInline);
            }
        }
    }
}