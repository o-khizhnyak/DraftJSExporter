using System.Collections.Generic;
using System.Linq;

namespace DraftJSExporter.Defaults
{
    /// <summary>Creates <see cref="HtmlElement"/></summary>
    /// <param name="text">Text</param>
    /// <param name="depth">Value of <see cref="Block.Depth"/> from <see cref="ContentState"/></param>
    /// <param name="prevDepth">Previous block depth</param>
    /// <param name="firstChild">Is this first child of its parent</param>
    public delegate HtmlElement CreateHtmlElement(int depth, int prevDepth = 0, bool firstChild = false);
    
    public class BlockMap
    {
        public CreateHtmlElement Unstyled { get; } = GetFactory("div");

        public CreateHtmlElement HeaderOne { get; } = GetFactory("h1");

        public CreateHtmlElement HeaderTwo { get; } = GetFactory("h2");

        public CreateHtmlElement HeaderThree { get; } = GetFactory("h3");

        public CreateHtmlElement HeaderFour { get; } = GetFactory("h4");

        public CreateHtmlElement HeaderFive { get; } = GetFactory("h5");

        public CreateHtmlElement HeaderSix { get; } = GetFactory("h6");

        public CreateHtmlElement UnorderedListItem { get; } = CreateListItem();

        public CreateHtmlElement OrderedListItem { get; } = CreateListItem();

        public CreateHtmlElement Blockquote { get; } = GetFactory("blockquote");

        public CreateHtmlElement Pre { get; } = GetFactory("pre");
        
        public CreateHtmlElement Atomic { get; } = GetFactory(null);

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

        public string this[string propertyName] => 
            (string)PropertyExpression<BlockMap>.GetValue(this,
                propertyName.First().ToString().ToUpper() + propertyName.Substring(1).ToLower());
    }
}