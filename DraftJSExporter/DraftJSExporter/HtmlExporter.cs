using System.Collections.Generic;
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
            IReadOnlyDictionary<string, string> attributes = null;
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
                    attributes = element.Attributes;
                    break;
                default:
                    tagName = "";
                    break;
            }
            
            TagBuilder.AddOpeningTag(sb, tagName, attributes, level, node.IsInline, addTab, selfClosing);
            
            if (selfClosing)
            {
                TagBuilder.CloseTag(sb, parentIsLastChild);
            }
            else
            {
                if (node.Text != null)
                {
                    TagBuilder.AddText(sb, node.Text, node.IsInline, level);         
                }
                
                var inline = false;
                var childrenCount = node.Children.Count;
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = node.Children[i];
                    RenderElement(sb, child, node.Name == null && node.Text == null ? level : level + 1, 
                        !(inline && child.IsInline) && !(child.Name == null && !child.IsInline) && !node.IsInline, 
                        i == childrenCount - 1, node.IsInline, lastChild);
                    inline = child.IsInline;
                }

                TagBuilder.AddClosingTag(sb, tagName, level, node.IsInline, lastChild, parentIsInline);
            }
        }
    }
}
