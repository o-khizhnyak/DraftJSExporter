using System.Collections.Generic;
using DraftJs.Exporter.Html.Models;
using HtmlTags;

namespace DraftJs.Exporter.Html.Defaults
{
    public class BlockMap
    {
        public CreateBlockTag Unstyled { get; set; } = GetFactory("div");

        public CreateBlockTag HeaderOne { get; set; } = GetFactory("h1");

        public CreateBlockTag HeaderTwo { get; set; } = GetFactory("h2");

        public CreateBlockTag HeaderThree { get; set; } = GetFactory("h3");

        public CreateBlockTag HeaderFour { get; set; } = GetFactory("h4");

        public CreateBlockTag HeaderFive { get; set; } = GetFactory("h5");

        public CreateBlockTag HeaderSix { get; set; } = GetFactory("h6");

        public CreateBlockTag UnorderedListItem { get; set; } = CreateListItem;

        public CreateBlockTag OrderedListItem { get; set; } = CreateListItem;

        public CreateBlockTag Blockquote { get; set; } = GetFactory("blockquote");

        public CreateBlockTag Pre { get; set; } = GetFactory("pre");
        
        public CreateBlockTag Atomic { get; set; } = (depth, prevDepth, firstChild) => new HtmlTag(null).NoTag();

        private static CreateBlockTag GetFactory(string tagName)
        {
            return (depth, prevDepth, firstChild) => new HtmlTag(tagName);
        }

        private static HtmlTag CreateListItem(int depth, int prevDepth, bool firstChild)
        {
            return new HtmlTag("li", t => ConfigureListItemTag(t, depth, prevDepth, firstChild));
        }

        private static void ConfigureListItemTag(HtmlTag t, int depth, int prevDepth, bool firstChild)
        {
            t.Attr("class", GetListItemClasses(depth, prevDepth, firstChild));
        }

        private static string GetListItemClasses(int depth, int prevDepth, bool firstChild)
        {
            return $"list-item--depth-{depth}{GetListItemResetClass(depth, prevDepth, firstChild)}";
        }

        private static string GetListItemResetClass(int depth, int prevDepth = 0, bool firstChild = false)
        {
            return firstChild || depth > prevDepth ? " list-item--reset" : "";
        }
    }
}