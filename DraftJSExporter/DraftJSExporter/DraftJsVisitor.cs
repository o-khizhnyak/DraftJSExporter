using System;
using System.Collections.Generic;

namespace DraftJSExporter
{
    public class DraftJsVisitor
    {
        public virtual void Visit(DraftJSTreeNode node, DraftJSTreeNode prevNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            switch (node)
            {
                case BlockTreeNode blockTreeNode:
                    VisitBlock(blockTreeNode, prevNode is BlockTreeNode prevBlockTreeNode ? prevBlockTreeNode : null);
                    break;
                case StyleTreeNode styleTreeNode:
                    VisitStyle(styleTreeNode);
                    break;
                case EntityTreeNode entityJsTreeNode:
                    VisitEntity(entityJsTreeNode);
                    break;
                case TextTreeNode textTreeNode:
                    VisitTextNode(textTreeNode);
                    break;
                default: throw new Exception($"Unknown node type: {node.GetType()}");
            }
        }

        public virtual void Visit(DraftJSRootNode node)
        {
            VisitArray(node.Children);
        }
        
        public virtual void VisitChildren(DraftJSTreeNode node) => VisitArray(node.Children);
        
        protected virtual void VisitArray(IEnumerable<DraftJSTreeNode> nodes)
        {
            DraftJSTreeNode prevNode = null;
            foreach (var node in nodes)
            {
                Visit(node, prevNode);
                prevNode = node;
            }
        }
        
        protected virtual void VisitBlock(BlockTreeNode node, BlockTreeNode prevNode)
        {
            switch (node)
            {
                case UnstyledBlock unstyled:
                    VisitUnstyled(unstyled);
                    break;
                case HeaderOneBlock headerOneBlock:
                    VisitHeaderOne(headerOneBlock);
                    break;
                case HeaderTwoBlock headerTwoBlock:
                    VisitHeaderTwo(headerTwoBlock);
                    break;
                case HeaderThreeBlock headerThreeBlock:
                    VisitHeaderThree(headerThreeBlock);
                    break;
                case HeaderFourBlock headerFourBlock:
                    VisitHeaderFour(headerFourBlock);
                    break;
                case HeaderFiveBlock headerFiveBlock:
                    VisitHeaderFive(headerFiveBlock);
                    break;
                case HeaderSixBlock headerSixBlock:
                    VisitHeaderSix(headerSixBlock);
                    break;
                case UnorderedListItemBlock unorderedListItemBlock:
                    VisitUnorderedListItem(unorderedListItemBlock, 
                        prevNode is UnorderedListItemBlock prevUnorderedListItem ? prevUnorderedListItem : null);
                    break;
                case OrderedListItemBlock orderedListItemBlock:
                    VisitOrderedListItem(orderedListItemBlock, 
                        prevNode is OrderedListItemBlock prevOrderedListItem ? prevOrderedListItem : null);
                    break;
                case BlockquoteBlock blockquoteBlock:
                    VisitBlockquote(blockquoteBlock);
                    break;
                case PreBlock preBlock:
                    VisitPre(preBlock);
                    break;
                case AtomicBlock atomicBlock:
                    VisitAtomic(atomicBlock);
                    break;
                default:
                    throw new Exception($"Unknown block type: {node.GetType()}");
            }
        }

        protected virtual void VisitStyle(StyleTreeNode node)
        {
            switch (node)
            {
                case BoldStyleTreeNode boldStyleTreeNode:
                    VisitBoldStyle(boldStyleTreeNode);
                    break;
                case CodeStyleTreeNode codeStyleTreeNode:
                    VisitCodeStyle(codeStyleTreeNode);
                    break;
                case ItalicStyleTreeNode italicStyleTreeNode:
                    VisitItalicStyle(italicStyleTreeNode);
                    break;
                case UnderlineStyleTreeNode underlineStyleTreeNode:
                    VisitUnderlineStyle(underlineStyleTreeNode);
                    break;
                case StrikethroughStyleTreeNode strikethroughStyleTreeNode:
                    VisitStrikethroughStyle(strikethroughStyleTreeNode);
                    break;
                case SuperscriptStyleTreeNode superscriptStyleTreeNode:
                    VisitSuperscriptStyle(superscriptStyleTreeNode);
                    break;
                case SubscriptStyleTreeNode subscriptStyleTreeNode:
                    VisitSubscriptStyle(subscriptStyleTreeNode);
                    break;
                case MarkStyleTreeNode markStyleTreeNode:
                    VisitMarkStyle(markStyleTreeNode);
                    break;
                case QuotationStyleTreeNode quotationStyleTreeNode:
                    VisitQuotationStyle(quotationStyleTreeNode);
                    break;
                case SmallStyleTreeNode smallStyleTreeNode:
                    VisitSmallStyle(smallStyleTreeNode);
                    break;
                case SampleStyleTreeNode sampleStyleTreeNode:
                    VisitSampleStyle(sampleStyleTreeNode);
                    break;
                case InsertStyleTreeNode insertStyleTreeNode:
                    VisitInsertStyle(insertStyleTreeNode);
                    break;
                case DeleteStyleTreeNode deleteStyleTreeNode:
                    VisitDeleteStyle(deleteStyleTreeNode);
                    break;
                case KeyboardStyleTreeNode keyboardStyleTreeNode:
                    VisitKeyboardStyle(keyboardStyleTreeNode);
                    break;
                default:
                    throw new Exception($"Unknown style type: {node.GetType()}");
            }
        }

        protected virtual void VisitEntity(EntityTreeNode node)
        {
        }
        
        protected virtual void VisitTextNode(TextTreeNode node)
        {
        }
        
        protected virtual void VisitUnstyled(UnstyledBlock node)
        {
        }
        
        protected virtual void VisitHeaderOne(HeaderOneBlock node)
        {
        }
        
        protected virtual void VisitHeaderTwo(HeaderTwoBlock node)
        {
        }
        
        protected virtual void VisitHeaderThree(HeaderThreeBlock node)
        {
        }
        
        protected virtual void VisitHeaderFour(HeaderFourBlock node)
        {
        }
        
        protected virtual void VisitHeaderFive(HeaderFiveBlock node)
        {
        }
        
        protected virtual void VisitHeaderSix(HeaderSixBlock node)
        {
        }
        
        protected virtual void VisitUnorderedListItem(UnorderedListItemBlock node, UnorderedListItemBlock prevNode)
        {
        }

        protected virtual void VisitOrderedListItem(OrderedListItemBlock node, OrderedListItemBlock prevNode)
        {
        }

        protected virtual void VisitBlockquote(BlockquoteBlock node)
        {
        }
        
        protected virtual void VisitPre(PreBlock node)
        {
        }
        
        protected virtual void VisitAtomic(AtomicBlock node)
        {
        }
        
        protected virtual void VisitBoldStyle(BoldStyleTreeNode node)
        {
        }
        
        protected virtual void VisitCodeStyle(CodeStyleTreeNode node)
        {
        }
        
        protected virtual void VisitItalicStyle(ItalicStyleTreeNode node)
        {
        }
        
        protected virtual void VisitUnderlineStyle(UnderlineStyleTreeNode node)
        {
        }
        
        protected virtual void VisitStrikethroughStyle(StrikethroughStyleTreeNode node)
        {
        }
        
        protected virtual void VisitSuperscriptStyle(SuperscriptStyleTreeNode node)
        {
        }
        
        protected virtual void VisitSubscriptStyle(SubscriptStyleTreeNode node)
        {
        }
        
        protected virtual void VisitMarkStyle(MarkStyleTreeNode node)
        {
        }
        
        protected virtual void VisitQuotationStyle(QuotationStyleTreeNode node)
        {
        }
        
        protected virtual void VisitSmallStyle(SmallStyleTreeNode node)
        {
        }
        
        protected virtual void VisitSampleStyle(SampleStyleTreeNode node)
        {
        }
        
        protected virtual void VisitInsertStyle(InsertStyleTreeNode node)
        {
        }
        
        protected virtual void VisitDeleteStyle(DeleteStyleTreeNode node)
        {
        }
        
        protected virtual void VisitKeyboardStyle(KeyboardStyleTreeNode node)
        {
        }
    }
}