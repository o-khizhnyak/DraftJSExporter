namespace DraftJSExporter.Defaults
{
    public class BlockMap
    {
        public string Unstyled { get; set; } = "p";

        public string HeaderOne { get; set; } = "h1";

        public string HeaderTwo { get; set; } = "h2";

        public string HeaderThree { get; set; } = "h3";

        public string HeaderFour { get; set; } = "h4";

        public string HeaderFive { get; set; } = "h5";

        public string HeaderSix { get; set; } = "h6";

        public ListItem UnorderedListItem { get; set; } = new ListItem
        {
            Element = "li",
            Wrapper = "ul"
        };

        public ListItem OrderedListItem { get; set; } = new ListItem
        {
            Element = "li",
            Wrapper = "ol"
        };

        public string Blockquote { get; set; } = "blockquote";

        public string Pre { get; set; } = "pre";
    }
}