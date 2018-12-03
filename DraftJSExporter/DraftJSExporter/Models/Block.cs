using System.Collections.Generic;
using System.Linq;

namespace DraftJSExporter
{
    public class Block
    {
        public string Key { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }
        
        public int Depth { get; set; }

        public List<InlineStyleRange> InlineStyleRanges { get; set; }

        public List<EntityRange> EntityRanges { get; set; }

        public Element ConvertToElement()
        {
            if (InlineStyleRanges.Count == 0 && EntityRanges.Count == 0)
            {
                return new Element(Type, new Dictionary<string, string>(), Text);
            }
            
            var styledParts = new Dictionary<string, List<InlineStyleRange>>();
            
            var startIndexes = InlineStyleRanges.Select(x => x.Offset);
            var stopIndexes = InlineStyleRanges.Select(x => x.Offset + x.Length);
            var indexes = startIndexes.Union(stopIndexes).Distinct().ToList();

            foreach (var index in indexes)
            {
                if (index > 0 && styledParts.Count == 0)
                {
                    styledParts.Add(Text.Substring(0, index), null);
                }
            }
            
        }
    }
}