using System.Collections.Generic;
using DraftJs.Exporter.Html.Defaults;
using DraftJs.Exporter.Html.Models;
using DraftJs.Exporter.Models;
using HtmlTags;

namespace DraftJs.Exporter.Html
{
    public class HtmlDraftJsVisitor : DraftJsVisitor
    {
        private readonly HtmlDraftJsExporterConfig _config;
        private readonly HtmlTag _root;
        private HtmlTag _current;

        public HtmlDraftJsVisitor(HtmlDraftJsExporterConfig config)
        {
            _root = new HtmlTag("div");
            _current = _root;
            _config = config;
        }

        public string Render(DraftJsRootNode node)
        {
            VisitRoot(node);
            return _root.ToHtmlString();
        }

        protected override void VisitArray(IEnumerable<DraftJsTreeNode> nodes)
        {
            DraftJsTreeNode prev = null;
            var prevParent = _current;
            foreach (var node in nodes)
            {
                if (node is OrderedListItemBlock && !(prev is OrderedListItemBlock))
                {
                    var ol = new HtmlTag("ol");
                    _current.Append(ol);
                    _current = ol;
                }
                else if (node is UnorderedListItemBlock && !(prev is UnorderedListItemBlock))
                {
                    var ul = new HtmlTag("ul");
                    _current.Append(ul);
                    _current = ul;
                }
                else if (!(node is OrderedListItemBlock) && prev is OrderedListItemBlock)
                {
                    _current = prevParent;
                }
                else if (!(node is UnorderedListItemBlock) && prev is UnorderedListItemBlock)
                {
                    _current = prevParent;
                }
                Visit(node, prev);

                prev = node;
            }

            _current = prevParent;
        }

        protected override void VisitEntity(EntityTreeNode node)
        {
            var tag = _config.EntityDecorators[node.Type](node.Data);
            RenderNode(node, tag);
        }

        protected override void VisitTextNode(TextTreeNode node)
        {
            if (_current.HasClosingTag())
            {
                _current.Append(new HtmlTag(string.Empty).NoTag().Text(node.Text));
            }
        }

        protected override void VisitUnstyled(UnstyledBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.Unstyled);
        }
        
        protected override void VisitHeaderOne(HeaderOneBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.HeaderOne);
        }
        
        protected override void VisitHeaderTwo(HeaderTwoBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.HeaderTwo);
        }
        
        protected override void VisitHeaderThree(HeaderThreeBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.HeaderThree);
        }
        
        protected override void VisitHeaderFour(HeaderFourBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.HeaderFour);
        }
        
        protected override void VisitHeaderFive(HeaderFiveBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.HeaderFive);
        }
        
        protected override void VisitHeaderSix(HeaderSixBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.HeaderSix);
        }
        
        protected override void VisitUnorderedListItem(UnorderedListItemBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.UnorderedListItem, prevNode?.Depth ?? 0, 
                !(prevNode is UnorderedListItemBlock));
        }

        protected override void VisitOrderedListItem(OrderedListItemBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.OrderedListItem, prevNode?.Depth ?? 0, 
                !(prevNode is OrderedListItemBlock));
        }
        
        protected override void VisitBlockquote(BlockquoteBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.Blockquote);
        }
        
        protected override void VisitPre(PreBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.Pre);
        }
        
        protected override void VisitAtomic(AtomicBlock node, BlockTreeNode prevNode)
        {
            RenderBlock(node, _config.BlockMap.Atomic);
        }

        protected override void VisitBoldStyle(BoldStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Bold);
        }
        
        protected override void VisitCodeStyle(CodeStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Code);
        }
        
        protected override void VisitItalicStyle(ItalicStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Italic);
        }
        
        protected override void VisitUnderlineStyle(UnderlineStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Underline);
        }
        
        protected override void VisitStrikethroughStyle(StrikethroughStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Strikethrough);
        }
        
        protected override void VisitSuperscriptStyle(SuperscriptStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Superscript);
        }
        
        protected override void VisitSubscriptStyle(SubscriptStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Subscript);
        }
        
        protected override void VisitMarkStyle(MarkStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Mark);
        }
        
        protected override void VisitQuotationStyle(QuotationStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Quotation);
        }
        
        protected override void VisitSmallStyle(SmallStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Small);
        }
        
        protected override void VisitSampleStyle(SampleStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Sample);
        }
        
        protected override void VisitInsertStyle(InsertStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Insert);
        }
        
        protected override void VisitDeleteStyle(DeleteStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Delete);
        }
        
        protected override void VisitKeyboardStyle(KeyboardStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Keyboard);
        }

        private void RenderBlock(BlockTreeNode node, CreateBlockTag createBlockTag, int prevDepth = 0, 
            bool firstChild = false)
        {
            var tag = createBlockTag(node.Depth, prevDepth, firstChild);
            RenderNode(node, tag);
        }

        private void RenderNode(DraftJsTreeNode node, HtmlTag tag)
        {
            var prev = _current;
            _current.Append(tag);
            _current = tag;
            VisitChildren(node);
            _current = prev;
        }
        
        private void RenderNode(DraftJsTreeNode node, string tagName)
        {
            RenderNode(node, new HtmlTag(tagName));
        }
    }
}