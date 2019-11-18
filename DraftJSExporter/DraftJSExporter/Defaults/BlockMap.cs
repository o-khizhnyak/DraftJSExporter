using System.Linq;

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

        public string UnorderedListItem { get; } = "li";

        public string OrderedListItem { get; } = "li";

        public string Blockquote { get; } = "blockquote";

        public string Pre { get; } = "pre";
        
        public string Atomic { get; } = null;

        public string this[string propertyName] => 
            (string)PropertyExpression<BlockMap>.GetValue(this,
                propertyName.First().ToString().ToUpper() + propertyName.Substring(1).ToLower());
    }
}