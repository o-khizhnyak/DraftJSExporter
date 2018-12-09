using System;
using System.Collections.Generic;
using DraftJSExporter.Defaults;

namespace DraftJSExporter
{
    public class ExporterConfig
    {
        public Dictionary<string, Func<IReadOnlyDictionary<string, string>, Element>> EntityDecorators { get; set; } = 
            new Dictionary<string, Func<IReadOnlyDictionary<string, string>, Element>>();

        public StyleMap StyleMap { get; set; } = new StyleMap();

        public BlockMap BlockMap { get; set; } = new BlockMap();
    }
}
