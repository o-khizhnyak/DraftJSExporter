using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DraftJs.Abstractions
{
    [PublicAPI]
    public class Block
    {
        private static readonly IReadOnlyCollection<IInterval> EmptyOffsetLength = Array.Empty<IInterval>();
        public string Text { get; set; }

        public string Type { get; set; }
        
        public int Depth { get; set; }

        public List<InlineStyleRange> InlineStyleRanges { get; set; }

        public List<EntityRange> EntityRanges { get; set; }

    }
}