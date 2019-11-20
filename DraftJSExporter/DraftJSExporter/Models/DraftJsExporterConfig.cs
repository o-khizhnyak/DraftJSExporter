using System;
using System.Collections.Generic;
using DraftJSExporter.Defaults;

namespace DraftJSExporter
{
    public class DraftJsExporterConfig
    {
        public Dictionary<string, Func<IReadOnlyDictionary<string, string>, HtmlElement>> EntityDecorators { get; set; } = 
            new Dictionary<string, Func<IReadOnlyDictionary<string, string>, HtmlElement>>();

        public StyleMap StyleMap { get; } = new StyleMap();

        public BlockMap BlockMap { get; } = new BlockMap();
    }
}
