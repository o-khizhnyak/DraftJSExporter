using System;
using System.Collections.Generic;

namespace DraftJSExporter.Defaults
{
    public class ListItem
    {
        public Func<int, int, string, bool, HtmlElement> Element { get; set; }

        public string Wrapper { get; set; }

        public static readonly Func<int, int, string, bool, HtmlElement> DefaultListItem = 
            (depth, prevDepth, text, firstChild) => new HtmlElement("li", new Dictionary<string, string>
                {
                    {"class", $"list-item--depth-{depth}{GetResetClass(depth, prevDepth, firstChild)}"}
                }, text);

        private static string GetResetClass(int depth, int prevDepth, bool firstChild) => 
            firstChild || depth > prevDepth ? " list-item--reset" : "";
    }
}