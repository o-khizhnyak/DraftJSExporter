using System;
using System.Collections.Generic;
using System.Text;

namespace DraftJSExporter
{
    public abstract class DraftTreeNode
    {
        public IReadOnlyList<DraftTreeNode> Children => _children;
        
        private readonly List<DraftTreeNode> _children = new List<DraftTreeNode>();
        
        public string Text { get; set; }

        public void AppendChild(DraftTreeNode node)
        {
            _children.Add(node);
        }
    }

    public class RootNode
    {
        public RootNode(IReadOnlyList<DraftTreeNode> children)
        {
            Children = children;
        }

        public IReadOnlyList<DraftTreeNode> Children { get; }
    }

    public abstract class BlockTreeNode: DraftTreeNode
    {   
    }

    public abstract class EntityTreeNode : DraftTreeNode
    {
        public IReadOnlyDictionary<string, string> Data { get; set; }

    }

    public class UnstyledBlock : BlockTreeNode
    {
    }

    public abstract class ListItemBlock : BlockTreeNode
    {
        public int Depth { get; set; }
    }

    public class UnorderedListItem : ListItemBlock
    {
        
    }
    
    public class OrderedListItem : ListItemBlock
    {
        
    }
    public abstract class StyleTreeNode: DraftTreeNode
    {
    }
    
    
    public class BoldStyleTreeNode: StyleTreeNode
    {
    }
    
    
    public class DraftJsVisitor
    {
        public virtual void Visit(DraftTreeNode node)
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
                default: throw new Exception($"Неизвестный тип ноды {node.GetType()}");
            }
        }

        public virtual void Visit(RootNode node)
        {
            VisitArray(node.Children);
        }
        
        public virtual void VisitChildren(DraftTreeNode node) => VisitArray(node.Children);
        
        protected virtual void VisitArray(IEnumerable<DraftTreeNode> nodes)
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
        }

        protected virtual void VisitUnstyled(UnstyledBlock node)
        {
            VisitChildren(node);
        }

        protected virtual void VisitStyle(StyleTreeNode node)
        {
            switch (node)
            {
                case BoldStyleTreeNode boldStyleTreeNode:
                    break;
            }
        }

        protected virtual void VisitEntity()
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

    public class HtmlDraftJsVisitor : DraftJsVisitor
    {
        private readonly StringBuilder _sb;
        public HtmlDraftJsVisitor()
        {
            _sb = new StringBuilder();
        }

        public string Render(RootNode node)
        {
            Visit(node);
            return _sb.ToString();
        }

        public static string RenderNode(RootNode node)
        {
            return new HtmlDraftJsVisitor().Render(node);
        }

        protected override void VisitArray(IEnumerable<DraftTreeNode> nodes)
        {
            DraftTreeNode prev = null;
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
    }


    public class HtmlExporter
    {
        public HtmlExporter(ExporterConfig config)
        {
            _config = config;
        }
        
        private ExporterConfig _config;
        private bool openedList = false;
        
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
