using System.Collections.Generic;

namespace DraftJSExporter
{
    public class TreeNode
    {
        public string Name { get; }
        
        public TreeNodeType Type { get; }
        
        public List<TreeNode> Children { get; }
        
        public string Text { get; set; }
        
        public int Depth { get; }
        
        public int PrevDepth { get; }
        
        public IReadOnlyDictionary<string, string> Data { get; }
        
        public TreeNode(string name = null, TreeNodeType type = TreeNodeType.Block, string text = null, int depth = 0, 
            int prevDepth = 0, IReadOnlyDictionary<string, string> data = null)
        {
            Name = name;
            Type = type;
            Text = text;
            Depth = depth;
            PrevDepth = prevDepth;
            Data = data ?? new Dictionary<string, string>();
            Children = new List<TreeNode>();
        }

        public void AppendChild(TreeNode child)
        {
            Children.Add(child);
        }
    }

    public enum TreeNodeType
    {
        Block,
        Style,
        Entity
    }
}
