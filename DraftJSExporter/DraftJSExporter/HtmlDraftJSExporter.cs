namespace DraftJSExporter
{
    public class HtmlDraftJSExporter
    {
        public HtmlDraftJSExporter(DraftJsExporterConfig config)
        {
            _config = config;
        }
        
        private DraftJsExporterConfig _config;
        
        public string Render(string contentStateJson)
        {
            var tree = new ContentStateToTreeConverter().Convert(contentStateJson);
            return new HtmlDraftJsVisitor(_config).Render(tree);
        }

    }
}
