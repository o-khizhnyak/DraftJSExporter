using System.Collections.Generic;
using System.Text;

namespace DraftJSExporter
{
    public class HtmlDraftJSExporter
    {
        public HtmlDraftJSExporter(ExporterConfig config)
        {
            _config = config;
        }
        
        private ExporterConfig _config;
        
        public string Render(string contentStateJson)
        {
            var tree = new ContentStateToTreeConverter().Convert(contentStateJson);
            
            var sb = new StringBuilder();
            RenderElement(sb, tree, 0,  true, false, true);
            return sb.ToString();
        }

        private void RenderElement(StringBuilder sb, TreeNode node, int level, bool lastChild, 
            bool parentIsInline, bool parentIsLastChild)
        {
            string tagName;
            IReadOnlyDictionary<string, string> attributes = null;
            var selfClosing = false;
            var isInline = false;

            switch (node.Type)
            {
                case TreeNodeType.Block:
                    tagName = _config.BlockMap[node.Name];
                    break;
                case TreeNodeType.Style:
                    tagName = _config.StyleMap[node.Name];
                    isInline = true;
                    break;
                case TreeNodeType.Entity:
                    var element = _config.EntityDecorators[node.Name](node.Data);
                    tagName = element.Type;
                    selfClosing = element.SelfClosing;
                    attributes = element.Attributes;
                    isInline = element.Inline;
                    break;
                default:
                    tagName = null;
                    break;
            }
            
            TagBuilder.AddOpeningTag(sb, tagName, attributes, level, isInline, selfClosing);
            
            if (selfClosing)
            {
                TagBuilder.CloseTag(sb, parentIsLastChild, level);
            }
            else
            {
                if (node.Text != null)
                {
                    TagBuilder.AddText(sb, node.Text, isInline, level);         
                }
                
                var childrenCount = node.Children.Count;
                
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = node.Children[i];
                    RenderElement(sb, child, node.Name == null && node.Text == null ? level : level + 1,
                        i == childrenCount - 1, isInline, lastChild);
                }

                TagBuilder.AddClosingTag(sb, tagName, level, isInline, lastChild, parentIsInline);
            }
        }
    }
}
