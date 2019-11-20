using System.Collections.Generic;

namespace DraftJSExporter
{
    public class HtmlDraftJsVisitor : DraftJsVisitor
    {
        private readonly HtmlBuilder _builder;
        private readonly DraftJsExporterConfig _config;
        
        public HtmlDraftJsVisitor(DraftJsExporterConfig config)
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
                Visit(node);

                prev = node;
            }
        }

        protected override void VisitEntity(EntityTreeNode node)
        {
            
        }

        protected override void VisitUnstyled(UnstyledBlock node)
        {
            RenderBlock(_config.BlockMap.Unstyled, node, false);
        }
        
        protected override void VisitHeaderOne(HeaderOneBlock node)
        {
            RenderBlock(_config.BlockMap.HeaderOne, node, false);
        }
        
        protected override void VisitHeaderTwo(HeaderTwoBlock node)
        {
            RenderBlock(_config.BlockMap.HeaderTwo, node, false);
        }
        
        protected override void VisitHeaderThree(HeaderThreeBlock node)
        {
            RenderBlock(_config.BlockMap.HeaderThree, node, false);
        }
        
        protected override void VisitHeaderFour(HeaderFourBlock node)
        {
            RenderBlock(_config.BlockMap.HeaderFour, node, false);
        }
        
        protected override void VisitHeaderFive(HeaderFiveBlock node)
        {
            RenderBlock(_config.BlockMap.HeaderFive, node, false);
        }
        
        protected override void VisitHeaderSix(HeaderSixBlock node)
        {
            RenderBlock(_config.BlockMap.HeaderSix, node, false);
        }
        
        protected override void VisitUnorderedListItem(UnorderedListItemBlock node)
        {
            RenderBlock(_config.BlockMap.UnorderedListItem, node, false);
        }

        protected override void VisitOrderedListItem(OrderedListItemBlock node)
        {
            RenderBlock(_config.BlockMap.OrderedListItem, node, false);
        }
        
        protected override void VisitBlockquote(BlockquoteBlock node)
        {
            RenderBlock(_config.BlockMap.Blockquote, node, false);
        }
        
        protected override void VisitPre(PreBlock node)
        {
            RenderBlock(_config.BlockMap.Pre, node, false);
        }
        
        protected override void VisitAtomic(AtomicBlock node)
        {
            RenderBlock(_config.BlockMap.Atomic, node, false);
        }

        protected override void VisitBoldStyle(BoldStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Bold, node, true);
        }
        
        protected override void VisitCodeStyle(CodeStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Code, node, true);
        }
        
        protected override void VisitItalicStyle(ItalicStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Italic, node, true);
        }
        
        protected override void VisitUnderlineStyle(UnderlineStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Underline, node, true);
        }
        
        protected override void VisitStrikethroughStyle(StrikethroughStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Strikethrough, node, true);
        }
        
        protected override void VisitSuperscriptStyle(SuperscriptStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Superscript, node, true);
        }
        
        protected override void VisitSubscriptStyle(SubscriptStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Subscript, node, true);
        }
        
        protected override void VisitMarkStyle(MarkStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Mark, node, true);
        }
        
        protected override void VisitQuotationStyle(QuotationStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Quotation, node, true);
        }
        
        protected override void VisitSmallStyle(SmallStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Small, node, true);
        }
        
        protected override void VisitSampleStyle(SampleStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Sample, node, true);
        }
        
        protected override void VisitInsertStyle(InsertStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Insert, node, true);
        }
        
        protected override void VisitDeleteStyle(DeleteStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Delete, node, true);
        }
        
        protected override void VisitKeyboardStyle(KeyboardStyleTreeNode node)
        {
            RenderBlock(_config.StyleMap.Keyboard, node, true);
        }

        private void RenderBlock(string tagName, DraftJSTreeNode node, bool inline)
        {
            _builder.AddOpeningTag(tagName, null, inline, false);
            _builder.AddText(node.Text);
            VisitChildren(node);
            _builder.AddClosingTag(tagName, inline);
        }
    }
}