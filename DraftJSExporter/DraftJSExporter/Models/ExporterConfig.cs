using System.Collections.Generic;
using DraftJSExporter.Defaults;

namespace DraftJSExporter
{
    public class ExporterConfig
    {
        public EntityDecorators EntityDecorators { get; set; }

        public StyleMap StyleMap { get; set; }

        public BlockMap BlockMap { get; set; }
    }
}