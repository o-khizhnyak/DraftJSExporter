using System;
using System.Collections.Generic;

namespace DraftJSExporter.Defaults
{
    public class ListItem
    {
        public Func<int, string, Element> Element { get; set; }

        public string Wrapper { get; set; }

        public static readonly Func<int, string, Element> DefaultListItem = 
            (i, text) => new Element("li", new Dictionary<string, string>
                {
                    {"class", $"list-item--depth-{i}"}
                }, text);
    }
}