using System;
using System.Collections.Generic;

namespace DraftJSExporter.Defaults
{
    public class BlockMap
    {
        public string Unstyled { get; set; } = "div";

        public string HeaderOne { get; set; } = "h1";

        public string HeaderTwo { get; set; } = "h2";

        public string HeaderThree { get; set; } = "h3";

        public string HeaderFour { get; set; } = "h4";

        public string HeaderFive { get; set; } = "h5";

        public string HeaderSix { get; set; } = "h6";

        public ListItem UnorderedListItem { get; set; } = new ListItem
        {
            Element = ListItem.DefaultListItem,
            Wrapper = "ul"
        };

        public ListItem OrderedListItem { get; set; } = new ListItem
        {
            Element = ListItem.DefaultListItem,
            Wrapper = "ol"
        };

        public string Blockquote { get; set; } = "blockquote";

        public string Pre { get; set; } = "pre";

        public Element GenerateBlockElement(string type, int depth, int prevDepth, string text = null, 
            Element wrapper = null)
        {
            switch (type)
            {
                case "unstyled":
                    return new Element(Unstyled, null, text);
                
                case "header-one":
                    return new Element(HeaderOne, null, text);
                
                case "header-two":
                    return new Element(HeaderTwo, null, text);
                
                case "header-three":
                    return new Element(HeaderThree, null, text);
                
                case "header-four":
                    return new Element(HeaderFour, null, text);
                
                case "header-five":
                    return new Element(HeaderFive, null, text);
                
                case "header-six":
                    return new Element(HeaderSix, null, text);
                
                case "unordered-list-item":
                    var ulWrapper = wrapper ?? new Element(UnorderedListItem.Wrapper, null, null, false, true);
                    ulWrapper.AppendChild(UnorderedListItem.Element(depth, prevDepth, text, ulWrapper.Children.Count == 0));
                    return ulWrapper;
                
                case "ordered-list-item":
                    var olWrapper = wrapper ?? new Element(OrderedListItem.Wrapper, null, null, false, true);
                    olWrapper.AppendChild(OrderedListItem.Element(depth, prevDepth, text, olWrapper.Children.Count == 0));
                    return olWrapper;
                
                case "blockquote":
                    return new Element(Blockquote, null, text);
                
                case "pre":
                    return new Element(Pre, null, text);
                
                case "atomic":
                    return new Element();
                
                default:
                    return new Element("div", null, text);
            }
        }
    }
}