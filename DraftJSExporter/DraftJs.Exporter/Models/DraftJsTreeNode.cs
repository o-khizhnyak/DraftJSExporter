using System.Collections.Generic;

namespace DraftJs.Exporter.Models
{
    public abstract class DraftJsTreeNode
    {
        public IReadOnlyList<DraftJsTreeNode> Children => _children;
        
        private readonly List<DraftJsTreeNode> _children = new List<DraftJsTreeNode>();
        
        public string Text { get; set; }

        public void AppendChild(DraftJsTreeNode node)
        {
            _children.Add(node);
        }
    }
    
    public class DraftJsRootNode
    {
        public DraftJsRootNode(IReadOnlyList<DraftJsTreeNode> children)
        {
            Children = children;
        }

        public IReadOnlyList<DraftJsTreeNode> Children { get; }
    }

    public abstract class BlockTreeNode: DraftJsTreeNode
    {
        public int Depth { get; set; }
    }
    
    public abstract class StyleTreeNode: DraftJsTreeNode
    {
    }

    public class EntityTreeNode : DraftJsTreeNode
    {
        public EntityTreeNode(string type, IReadOnlyDictionary<string, string> data)
        {
            Type = type;
            Data = data;
        }

        public string Type { get; set; }
        public IReadOnlyDictionary<string, string> Data { get; set; }
    }

    public class TextTreeNode : DraftJsTreeNode
    {
        public TextTreeNode(string text)
        {
            Text = text;
        }
    }

    public class UnstyledBlock : BlockTreeNode
    {
    }

    public class HeaderOneBlock : BlockTreeNode
    {
    }
    
    public class HeaderTwoBlock : BlockTreeNode
    {
    }
    
    public class HeaderThreeBlock : BlockTreeNode
    {
    }
    
    public class HeaderFourBlock : BlockTreeNode
    {
    }
    
    public class HeaderFiveBlock : BlockTreeNode
    {
    }
    
    public class HeaderSixBlock : BlockTreeNode
    {
    }
    
    public class UnorderedListItemBlock : BlockTreeNode
    {
    }
    
    public class OrderedListItemBlock : BlockTreeNode
    {
    }
    
    public class BlockquoteBlock : BlockTreeNode
    {
    }
    
    public class PreBlock : BlockTreeNode
    {
    }
    
    public class AtomicBlock : BlockTreeNode
    {
    }
    
    public class BoldStyleTreeNode: StyleTreeNode
    {
    }
    
    public class CodeStyleTreeNode: StyleTreeNode
    {
    }
    
    public class ItalicStyleTreeNode: StyleTreeNode
    {
    }
    
    public class UnderlineStyleTreeNode: StyleTreeNode
    {
    }
    
    public class StrikethroughStyleTreeNode: StyleTreeNode
    {
    }
    
    public class SuperscriptStyleTreeNode: StyleTreeNode
    {
    }
    
    public class SubscriptStyleTreeNode: StyleTreeNode
    {
    }
    
    public class MarkStyleTreeNode: StyleTreeNode
    {
    }
    
    public class QuotationStyleTreeNode: StyleTreeNode
    {
    }
    
    public class SmallStyleTreeNode: StyleTreeNode
    {
    }
    
    public class SampleStyleTreeNode: StyleTreeNode
    {
    }
    
    public class InsertStyleTreeNode: StyleTreeNode
    {
    }
    
    public class DeleteStyleTreeNode: StyleTreeNode
    {
    }
    
    public class KeyboardStyleTreeNode: StyleTreeNode
    {
    }
}