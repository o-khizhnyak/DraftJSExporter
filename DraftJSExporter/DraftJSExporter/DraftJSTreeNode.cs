using System.Collections.Generic;

namespace DraftJSExporter
{
    public abstract class DraftJSTreeNode
    {
        public IReadOnlyList<DraftJSTreeNode> Children => _children;
        
        private readonly List<DraftJSTreeNode> _children = new List<DraftJSTreeNode>();
        
        public string Text { get; set; }

        public void AppendChild(DraftJSTreeNode node)
        {
            _children.Add(node);
        }
    }
    
    public class DraftJSRootNode
    {
        public DraftJSRootNode(IReadOnlyList<DraftJSTreeNode> children)
        {
            Children = children;
        }

        public IReadOnlyList<DraftJSTreeNode> Children { get; }
    }

    public abstract class BlockTreeNode: DraftJSTreeNode
    {
    }
    
    public abstract class StyleTreeNode: DraftJSTreeNode
    {
    }

    public class EntityTreeNode : DraftJSTreeNode
    {
        public IReadOnlyDictionary<string, string> Data { get; set; }
    }

    public class TextTreeNode : DraftJSTreeNode
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
    
    public abstract class ListItemBlock : BlockTreeNode
    {
        public int Depth { get; set; }
    }

    public class UnorderedListItemBlock : ListItemBlock
    {
    }
    
    public class OrderedListItemBlock : ListItemBlock
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