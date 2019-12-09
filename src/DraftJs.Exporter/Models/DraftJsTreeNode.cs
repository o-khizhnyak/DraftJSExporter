using System.Collections.Generic;

namespace DraftJs.Exporter.Models
{
    public abstract class DraftJsTreeNode
    {
        public IReadOnlyList<DraftJsTreeNode> Children => _children;
        
        private readonly List<DraftJsTreeNode> _children = new List<DraftJsTreeNode>();
        
        public void AppendChild(DraftJsTreeNode node)
        {
            _children.Add(node);
        }
        
        public void RemoveLastChild()
        {
            _children.RemoveAt(_children.Count - 1);
        }
    }
}