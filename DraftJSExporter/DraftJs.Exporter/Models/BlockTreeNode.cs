using System;

namespace DraftJs.Exporter.Models
{
    public abstract class BlockTreeNode: DraftJsTreeNode
    {
        protected BlockTreeNode(int depth)
        {
            Depth = depth;
        }

        public int Depth { get; }
        
        public static BlockTreeNode Create(string type, int depth)
        {
            return type switch
            {
                "unstyled" => (BlockTreeNode) new UnstyledBlock(depth),
                "header-one" => new HeaderOneBlock(depth),
                "header-two" => new HeaderTwoBlock(depth),
                "header-three" => new HeaderThreeBlock(depth),
                "header-four" => new HeaderFourBlock(depth),
                "header-five" => new HeaderFiveBlock(depth),
                "header-six" => new HeaderSixBlock(depth),
                "unordered-list-item" => new UnorderedListItemBlock(depth),
                "ordered-list-item" => new OrderedListItemBlock(depth),
                "blockquote" => new BlockquoteBlock(depth),
                "pre" => new PreBlock(depth),
                "atomic" => new AtomicBlock(depth),
                _ => throw new Exception($"Unknown block type: {type}")
            };
        }
    }
}