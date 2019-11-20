using System.Collections.Generic;
using System.Text;

namespace DraftJSExporter
{
    public class HtmlDraftJsVisitor : DraftJsVisitor
    {
        private readonly StringBuilder _sb;
        public HtmlDraftJsVisitor()
        {
            _sb = new StringBuilder();
        }

        public string Render(DraftJSRootNode node)
        {
            Visit(node);
            return _sb.ToString();
        }

        protected override void VisitArray(IEnumerable<DraftJSTreeNode> nodes)
        {
            DraftJSTreeNode prev = null;
            foreach (var node in nodes)
            {
                if (node is OrderedListItem _ && !(prev is OrderedListItem))
                {
                    TagBuilder.AddOpeningTag(_sb, "ol", null, 0, false, false);
                }
                else if (node is UnorderedListItem _ && !(prev is UnorderedListItem))
                {
                    TagBuilder.AddOpeningTag(_sb, "ul", null, 0, false, false);
                }
                else if(prev is OrderedListItem)
                {
                    TagBuilder.AddClosingTag(_sb, "ol", 0, false, false, false);
                }
                else if(prev is UnorderedListItem)
                {
                    TagBuilder.AddClosingTag(_sb, "ul", 0, false, false, false);
                }
                Visit(node);

                prev = node;
            }
        }

        protected override void VisitBlock(BlockTreeNode node)
        {
            TagBuilder.AddOpeningTag(_sb, );
        }
    }
}