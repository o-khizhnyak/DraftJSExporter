using System.Collections.Generic;

namespace DraftJSExporter
{
    public class ContentState
    {
        public List<Block> Blocks { get; set; }

        public Dictionary<int, Entity> EntityMap { get; set; }
    }
}