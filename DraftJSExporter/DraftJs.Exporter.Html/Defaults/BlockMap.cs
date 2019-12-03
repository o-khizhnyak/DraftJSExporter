using System.Collections.Generic;
using DraftJs.Abstractions;
using DraftJs.Exporter.Html.Models;

namespace DraftJs.Exporter.Html.Defaults
{
    /// <summary>Creates <see cref="HtmlElement"/></summary>
    /// <param name="depth">Value of <see cref="Block.Depth"/> from <see cref="ContentState"/></param>
    /// <param name="prevDepth">Previous block depth</param>
    /// <param name="firstChild">Is this first child of its parent</param>
    public delegate HtmlElement CreateHtmlElement(int depth, int prevDepth = 0, bool firstChild = false);
    
    public class BlockMap
    {
        public CreateHtmlElement Unstyled { get; set; } = GetFactory("div");

        public CreateHtmlElement HeaderOne { get; set; } = GetFactory("h1");

        public CreateHtmlElement HeaderTwo { get; set; } = GetFactory("h2");

        public CreateHtmlElement HeaderThree { get; set; } = GetFactory("h3");

        public CreateHtmlElement HeaderFour { get; set; } = GetFactory("h4");

        public CreateHtmlElement HeaderFive { get; set; } = GetFactory("h5");

        public CreateHtmlElement HeaderSix { get; set; } = GetFactory("h6");

        public CreateHtmlElement UnorderedListItem { get; set; } = CreateListItem();

        public CreateHtmlElement OrderedListItem { get; set; } = CreateListItem();

        public CreateHtmlElement Blockquote { get; set; } = GetFactory("blockquote");

        public CreateHtmlElement Pre { get; set; } = GetFactory("pre");
        
        public CreateHtmlElement Atomic { get; set; } = GetFactory(null);

        private static CreateHtmlElement GetFactory(string tagName)
        {
            return (depth, prevDepth, firstChild) => new HtmlElement(tagName);
        }

        private static CreateHtmlElement CreateListItem()
        {
            return (depth, prevDepth, firstChild) => new HtmlElement("li", new Dictionary<string, string>
            {
                {"class", $"list-item--depth-{depth}{GetListItemResetClass(depth, prevDepth, firstChild)}"}
            });
        }

        private static string GetListItemResetClass(int depth, int prevDepth = 0, bool firstChild = false)
        {
            return firstChild || depth > prevDepth ? " list-item--reset" : "";
        }
    }
}