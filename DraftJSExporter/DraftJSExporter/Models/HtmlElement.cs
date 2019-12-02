using System.Collections.Generic;

namespace DraftJSExporter
{
    public class HtmlElement
    {
        public string Type { get; }

        public IReadOnlyDictionary<string, string> Attributes { get; }

        public bool Inline { get; set; }

        public HtmlElement(string type = null, IReadOnlyDictionary<string, string> attr = null, bool inline = false)
        {
            Type = type;
            Attributes = attr ?? new Dictionary<string, string>();
            Inline = inline;
        }
    }
}