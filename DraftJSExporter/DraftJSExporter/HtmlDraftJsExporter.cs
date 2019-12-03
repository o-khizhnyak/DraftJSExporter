namespace DraftJSExporter
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

            if (tree == null)
            {
                return "";
            }
            
            return new HtmlDraftJsVisitor(_config).Render(tree);
        }

        public string Render(DraftJSRootNode rootNode)
        {
            return new HtmlDraftJsVisitor(_config).Render(rootNode);   
        }

    }
}
