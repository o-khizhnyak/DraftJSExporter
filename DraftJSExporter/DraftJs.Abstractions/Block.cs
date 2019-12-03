using System.Collections.Generic;

namespace DraftJs.Abstractions
{
    public class Block
    {
        public string Text { get; set; }

        public string Type { get; set; }
        
        public int Depth { get; set; }

        public List<InlineStyleRange> InlineStyleRanges { get; set; }

        public List<EntityRange> EntityRanges { get; set; }
    }
}