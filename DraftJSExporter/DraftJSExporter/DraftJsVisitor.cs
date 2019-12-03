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

        public virtual void VisitRoot(DraftJSRootNode node) => VisitArray(node.Children);

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
                    VisitUnstyled(unstyled, prevNode);
                    break;
                case HeaderOneBlock headerOneBlock:
                    VisitHeaderOne(headerOneBlock, prevNode);
                    break;
                case HeaderTwoBlock headerTwoBlock:
                    VisitHeaderTwo(headerTwoBlock, prevNode);
                    break;
                case HeaderThreeBlock headerThreeBlock:
                    VisitHeaderThree(headerThreeBlock, prevNode);
                    break;
                case HeaderFourBlock headerFourBlock:
                    VisitHeaderFour(headerFourBlock, prevNode);
                    break;
                case HeaderFiveBlock headerFiveBlock:
                    VisitHeaderFive(headerFiveBlock, prevNode);
                    break;
                case HeaderSixBlock headerSixBlock:
                    VisitHeaderSix(headerSixBlock, prevNode);
                    break;
                case UnorderedListItemBlock unorderedListItemBlock:
                    VisitUnorderedListItem(unorderedListItemBlock, prevNode);
                    break;
                case OrderedListItemBlock orderedListItemBlock:
                    VisitOrderedListItem(orderedListItemBlock, prevNode);
                    break;
                case BlockquoteBlock blockquoteBlock:
                    VisitBlockquote(blockquoteBlock, prevNode);
                    break;
                case PreBlock preBlock:
                    VisitPre(preBlock, prevNode);
                    break;
                case AtomicBlock atomicBlock:
                    VisitAtomic(atomicBlock, prevNode);
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
            VisitChildren(node);
        }
        
        protected virtual void VisitTextNode(TextTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitUnstyled(UnstyledBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitHeaderOne(HeaderOneBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitHeaderTwo(HeaderTwoBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitHeaderThree(HeaderThreeBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitHeaderFour(HeaderFourBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitHeaderFive(HeaderFiveBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitHeaderSix(HeaderSixBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitUnorderedListItem(UnorderedListItemBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }

        protected virtual void VisitOrderedListItem(OrderedListItemBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }

        protected virtual void VisitBlockquote(BlockquoteBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitPre(PreBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitAtomic(AtomicBlock node, BlockTreeNode prevNode)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitBoldStyle(BoldStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitCodeStyle(CodeStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitItalicStyle(ItalicStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitUnderlineStyle(UnderlineStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitStrikethroughStyle(StrikethroughStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitSuperscriptStyle(SuperscriptStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitSubscriptStyle(SubscriptStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitMarkStyle(MarkStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitQuotationStyle(QuotationStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitSmallStyle(SmallStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitSampleStyle(SampleStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitInsertStyle(InsertStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitDeleteStyle(DeleteStyleTreeNode node)
        {
            VisitChildren(node);
        }
        
        protected virtual void VisitKeyboardStyle(KeyboardStyleTreeNode node)
        {
            VisitChildren(node);
        }
    }
}