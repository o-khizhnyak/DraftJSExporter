namespace DraftJSExporter.Defaults
{
    public class BlockMap
    {
        public string Unstyled { get; } = "div";

        public string HeaderOne { get; } = "h1";

        public string HeaderTwo { get; } = "h2";

        public string HeaderThree { get; } = "h3";

        public string HeaderFour { get; } = "h4";

        public string HeaderFive { get; } = "h5";

        public string HeaderSix { get; } = "h6";

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

        public string Blockquote { get; } = "blockquote";

        public string Pre { get; } = "pre";

        public string GetTagName(string type)
        {
            switch (type)
            {
                case "unstyled":
                    return Unstyled;
                
                case "header-one":
                    return HeaderOne;
                
                case "header-two":
                    return HeaderTwo;
                
                case "header-three":
                    return HeaderThree;
                
                case "header-four":
                    return HeaderFour;
                
                case "header-five":
                    return HeaderFive;
                
                case "header-six":
                    return HeaderSix;
                
//                case "unordered-list-item":
//                    var ulWrapper = wrapper ?? new HtmlElement(UnorderedListItem.Wrapper, null, null, false, true);
//                    ulWrapper.AppendChild(UnorderedListItem.Element(depth, prevDepth, null, ulWrapper.Children.Count == 0));
//                    return ulWrapper;
//                
//                case "ordered-list-item":
//                    var olWrapper = wrapper ?? new HtmlElement(OrderedListItem.Wrapper, null, null, false, true);
//                    olWrapper.AppendChild(OrderedListItem.Element(depth, prevDepth, null, olWrapper.Children.Count == 0));
//                    return olWrapper;
                
                case "blockquote":
                    return Blockquote;
                
                case "pre":
                    return Pre;
                
                case "atomic":
                    return "";
                
                default:
                    return "div";
            }
        }
    }
}