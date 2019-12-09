using DraftJs.Exporter.Html.Models;
using DraftJs.Exporter.Models;
using HtmlTags;

namespace DraftJs.Exporter.Html
{
    public class HtmlDraftJsExporter
    {
        public HtmlDraftJsExporter(HtmlDraftJsExporterConfig config)
        {
            _config = config;
        }
        
        private readonly HtmlDraftJsExporterConfig _config;
        
        public string Render(string contentStateJson)
        {
            var tree = ContentStateToTreeConverter.Convert(contentStateJson);

            return tree == null ? "" : new HtmlDraftJsVisitor(_config).Render(tree);
        }

        public string Render(DraftJsRootNode rootNode)
        {
            return new HtmlDraftJsVisitor(_config).Render(rootNode);
        }
        
        public HtmlTag RenderTag(DraftJsRootNode rootNode)
        {
            return new HtmlDraftJsVisitor(_config).RenderTag(rootNode);
        }

    }
}
