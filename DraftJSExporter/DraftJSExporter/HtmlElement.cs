using System.Collections.Generic;

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
    }
}