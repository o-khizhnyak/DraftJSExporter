namespace DraftJSExporter
{
    public class HtmlDraftJsExporter
    {
        public HtmlDraftJsExporter(HtmlDraftJsExporterConfig config)
        {
            _config = config;
        }
        
        private HtmlDraftJsExporterConfig _config;
        
        public string Render(string contentStateJson)
        {
            var tree = new ContentStateToTreeConverter().Convert(contentStateJson);

            if (tree == null)
            {
                return "";
            }
            
            return new HtmlDraftJsVisitor(_config).Render(tree);
        }

    }
}
