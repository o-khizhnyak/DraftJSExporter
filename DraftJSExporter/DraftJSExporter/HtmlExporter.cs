using System.Text;

namespace DraftJSExporter
{
    public class HtmlExporter
    {
        public HtmlExporter(ExporterConfig config)
        {
            _config = config;
        }
        
        private ExporterConfig _config;
        
        public string Render(string contentStateJson)
        {
            var tree = new ContentStateToTreeConverter().Convert(contentStateJson);
            
            var sb = new StringBuilder();
            RenderElement(sb, tree, 0, true, true, false, true);
            return sb.ToString();
        }

        private void RenderElement(StringBuilder sb, TreeNode node, int level, bool addTab, bool lastChild, 
            bool parentIsInline, bool parentIsLastChild)
        {
            string tagName;
            var selfClosing = false;
            
            switch (node.Type)
            {
                case TreeNodeType.Block:
                    tagName = _config.BlockMap.GetTagName(node.Name);
                    break;
                case TreeNodeType.Style:
                    tagName = _config.StyleMap[node.Name];
                    break;
                case TreeNodeType.Entity:
                    var element = _config.EntityDecorators[node.Name](node.Data); 
                    tagName = element.Type;
                    selfClosing = element.SelfClosing;
                    break;
                default:
                    tagName = "";
                    break;
            }
            
            TagBuilder.AddOpeningTag(sb, tagName, node.Data, level, node.IsInline, addTab, selfClosing);
            
            
        }
    }
}
