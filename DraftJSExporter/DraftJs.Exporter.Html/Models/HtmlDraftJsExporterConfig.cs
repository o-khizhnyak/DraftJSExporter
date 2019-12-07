using System.Collections.Generic;
using DraftJs.Exporter.Html.Defaults;

namespace DraftJs.Exporter.Html.Models
{
    public class HtmlDraftJsExporterConfig
    {
        public Dictionary<string, CreateTagFromEntityData> EntityDecorators { get; set; } = new Dictionary<string, CreateTagFromEntityData>();

        public StyleMap StyleMap { get; } = new StyleMap();

        public BlockMap BlockMap { get; } = new BlockMap();
    }
}
