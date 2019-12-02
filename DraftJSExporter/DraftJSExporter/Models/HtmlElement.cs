using System.Collections.Generic;

namespace DraftJSExporter
{
    public class HtmlElement
    {
        public string Type { get; }

        public IReadOnlyDictionary<string, string> Attributes { get; }

        public bool Inline { get; set; }

        public bool SelfClosing { get; set; }

        public HtmlElement(string type = null, IReadOnlyDictionary<string, string> attr = null, bool inline = false, 
            bool selfClosing = false)
        {
            Type = type;
            Attributes = attr ?? new Dictionary<string, string>();
            Inline = inline;
            SelfClosing = selfClosing;
        }
    }
}