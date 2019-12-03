using System.Collections.Generic;

namespace DraftJs.Abstractions
{
    public class ContentState
    {
        public List<Block> Blocks { get; set; }

        public Dictionary<int, Entity> EntityMap { get; set; }
    }
}