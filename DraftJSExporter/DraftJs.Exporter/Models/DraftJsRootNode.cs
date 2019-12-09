using System.Collections.Generic;

namespace DraftJs.Exporter.Models
{
    public class DraftJsRootNode
    {
        public DraftJsRootNode(IReadOnlyList<DraftJsTreeNode> children)
        {
            Children = children;
        }

        public IReadOnlyList<DraftJsTreeNode> Children { get; }
    }
}