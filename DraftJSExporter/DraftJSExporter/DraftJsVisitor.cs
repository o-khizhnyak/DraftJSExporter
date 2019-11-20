using System;
using System.Collections.Generic;

namespace DraftJSExporter
{
    public class DraftJsVisitor
    {
        public virtual void Visit(DraftJSTreeNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            switch (node)
            {
                case BlockTreeNode blockTreeNode:
                    VisitBlock(blockTreeNode);
                    break;
                case StyleTreeNode styleTreeNode:
                    VisitStyle(styleTreeNode);
                    break;
                case EntityTreeNode entityJsTreeNode:
                    VisitEntity(entityJsTreeNode);
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
            foreach (var node in nodes)
            {
                Visit(node);
            }
        }
        
        protected virtual void VisitBlock(BlockTreeNode node)
        {
            switch (node)
            {
                case UnstyledBlock unstyled:
                    VisitUnstyled(unstyled);
                    break;
            }
            
            VisitChildren(node);
        }

        protected virtual void VisitStyle(StyleTreeNode node)
        {
            switch (node)
            {
                case BoldStyleTreeNode boldStyleTreeNode:
                    break;
            }
            
            VisitChildren(node);
        }

        protected virtual void VisitEntity(EntityTreeNode node)
        {
        }
        
        protected virtual void VisitUnstyled(UnstyledBlock node)
        {
        }

        protected virtual void VisitListItem(ListItemBlock block)
        {
            switch (block)
            {
                case null:
                    throw new ArgumentNullException(nameof(block));
                case OrderedListItem orderedListItem:
                    VisitOrderedListItem(orderedListItem);
                    break;
                case UnorderedListItem unorderedListItem:
                    VisitUnorderedListItem(unorderedListItem);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(block));
            }
        }

        protected virtual void VisitUnorderedListItem(UnorderedListItem unorderedListItem)
        {
        }

        protected virtual void VisitOrderedListItem(OrderedListItem orderedListItem)
        {
        }
    }
}