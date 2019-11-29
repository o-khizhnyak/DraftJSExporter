namespace DraftJSExporter
{
    public class HtmlDraftJSExporter
    {
        public HtmlDraftJSExporter(HtmlDraftJsExporterConfig config)
        {
            _config = config;
        }
        
        private HtmlDraftJsExporterConfig _config;
        
        public string Render(string contentStateJson)
        {
            var tree = new ContentStateToTreeConverter().Convert(contentStateJson);
            return new HtmlDraftJsVisitor(_config).Render(tree);
        }

    }
}
