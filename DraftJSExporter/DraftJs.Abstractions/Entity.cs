using System.Collections.Generic;

namespace DraftJs.Abstractions
{
    public class Entity
    {
        public string Type { get; set; }

        public string Mutability { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}