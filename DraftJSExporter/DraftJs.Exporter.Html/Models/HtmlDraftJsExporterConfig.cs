using System;
using System.Collections.Generic;
using DraftJs.Exporter.Html.Defaults;

namespace DraftJs.Exporter.Html.Models
{
    public class HtmlDraftJsExporterConfig
    {
        public Dictionary<string, Func<IReadOnlyDictionary<string, string>, HtmlElement>> EntityDecorators { get; set; } = 
            new Dictionary<string, Func<IReadOnlyDictionary<string, string>, HtmlElement>>();

        public StyleMap StyleMap { get; } = new StyleMap();

        public BlockMap BlockMap { get; } = new BlockMap();
    }
}
