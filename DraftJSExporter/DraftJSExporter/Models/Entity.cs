using System;
using System.Collections.Generic;

namespace DraftJSExporter
{
    public class Entity
    {
        public string Type { get; set; }

        public string Mutability { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}