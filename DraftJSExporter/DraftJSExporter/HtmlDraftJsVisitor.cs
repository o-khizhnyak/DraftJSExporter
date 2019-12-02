using System.Collections.Generic;
using DraftJSExporter.Defaults;

namespace DraftJSExporter
{
    public class HtmlDraftJsVisitor : DraftJsVisitor
    {
        private readonly HtmlBuilder _builder;
        private readonly HtmlDraftJsExporterConfig _config;
        
        public HtmlDraftJsVisitor(HtmlDraftJsExporterConfig config)
        {
            _builder = new HtmlBuilder();
            _config = config;
        }

        public string Render(DraftJSRootNode node)
        {
            Visit(node);
            return _builder.ToString();
        }

        protected override void VisitArray(IEnumerable<DraftJSTreeNode> nodes)
        {
            DraftJSTreeNode prev = null;
            foreach (var node in nodes)
            {
                if (node is OrderedListItemBlock _ && !(prev is OrderedListItemBlock))
                {
                    _builder.AddOpeningTag("ol", null, false, false);
                }
                else if (node is UnorderedListItemBlock _ && !(prev is UnorderedListItemBlock))
                {
                    _builder.AddOpeningTag("ul", null, false, false);
                }
                else if (prev is OrderedListItemBlock)
                {
                    _builder.AddClosingTag("ol", false);
                }
                else if(prev is UnorderedListItemBlock)
                {
                    _builder.AddClosingTag("ul", false);
                }
                Visit(node, prev);

                prev = node;
            }
        }

        protected override void VisitEntity(EntityTreeNode node)
        {
            var element = _config.EntityDecorators[node.Type](node.Data);
            RenderNode(node, element.Type, element.Inline, element.Attributes);
        }

        protected override void VisitTextNode(TextTreeNode node)
        {
            RenderNode(node, null, true);
        }

        protected override void VisitUnstyled(UnstyledBlock node)
        {
            RenderBlock(node, _config.BlockMap.Unstyled);
        }
        
        protected override void VisitHeaderOne(HeaderOneBlock node)
        {
            RenderBlock(node, _config.BlockMap.HeaderOne);
        }
        
        protected override void VisitHeaderTwo(HeaderTwoBlock node)
        {
            RenderBlock(node, _config.BlockMap.HeaderTwo);
        }
        
        protected override void VisitHeaderThree(HeaderThreeBlock node)
        {
            RenderBlock(node, _config.BlockMap.HeaderThree);
        }
        
        protected override void VisitHeaderFour(HeaderFourBlock node)
        {
            RenderBlock(node, _config.BlockMap.HeaderFour);
        }
        
        protected override void VisitHeaderFive(HeaderFiveBlock node)
        {
            RenderBlock(node, _config.BlockMap.HeaderFive);
        }
        
        protected override void VisitHeaderSix(HeaderSixBlock node)
        {
            RenderBlock(node, _config.BlockMap.HeaderSix);
        }
        
        protected override void VisitUnorderedListItem(UnorderedListItemBlock node, UnorderedListItemBlock prevNode)
        {
            RenderBlock(node, _config.BlockMap.UnorderedListItem, prevNode?.Depth ?? 0, prevNode == null);
        }

        protected override void VisitOrderedListItem(OrderedListItemBlock node, OrderedListItemBlock prevNode)
        {
            RenderBlock(node, _config.BlockMap.OrderedListItem, prevNode?.Depth ?? 0, prevNode == null);
        }
        
        protected override void VisitBlockquote(BlockquoteBlock node)
        {
            RenderBlock(node, _config.BlockMap.Blockquote);
        }
        
        protected override void VisitPre(PreBlock node)
        {
            RenderBlock(node, _config.BlockMap.Pre);
        }
        
        protected override void VisitAtomic(AtomicBlock node)
        {
            RenderBlock(node, _config.BlockMap.Atomic);
        }

        protected override void VisitBoldStyle(BoldStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Bold, true);
        }
        
        protected override void VisitCodeStyle(CodeStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Code, true);
        }
        
        protected override void VisitItalicStyle(ItalicStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Italic, true);
        }
        
        protected override void VisitUnderlineStyle(UnderlineStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Underline, true);
        }
        
        protected override void VisitStrikethroughStyle(StrikethroughStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Strikethrough, true);
        }
        
        protected override void VisitSuperscriptStyle(SuperscriptStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Superscript, true);
        }
        
        protected override void VisitSubscriptStyle(SubscriptStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Subscript, true);
        }
        
        protected override void VisitMarkStyle(MarkStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Mark, true);
        }
        
        protected override void VisitQuotationStyle(QuotationStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Quotation, true);
        }
        
        protected override void VisitSmallStyle(SmallStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Small, true);
        }
        
        protected override void VisitSampleStyle(SampleStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Sample, true);
        }
        
        protected override void VisitInsertStyle(InsertStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Insert, true);
        }
        
        protected override void VisitDeleteStyle(DeleteStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Delete, true);
        }
        
        protected override void VisitKeyboardStyle(KeyboardStyleTreeNode node)
        {
            RenderNode(node, _config.StyleMap.Keyboard, true);
        }

        private void RenderBlock(BlockTreeNode node, CreateHtmlElement createHtmlElement, int prevDepth = 0, 
            bool firstChild = false)
        {
            var htmlElement = createHtmlElement(node.Depth, prevDepth, firstChild);
            RenderNode(node, htmlElement.Type, false, htmlElement.Attributes);
        }

        private void RenderNode(DraftJSTreeNode node, string tagName,  bool inline, IReadOnlyDictionary<string, string> attributes = null)
        {
            _builder.AddOpeningTag(tagName, attributes, inline, false);
            _builder.AddText(node.Text);
            VisitChildren(node);
            _builder.AddClosingTag(tagName, inline);
        }
    }
}