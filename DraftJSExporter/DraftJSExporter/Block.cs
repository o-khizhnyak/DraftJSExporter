using System.Collections.Generic;
using System.Linq;

namespace DraftJSExporter
{
    public class Block
    {
        public string Text { get; set; }

        public string Type { get; set; }
        
        public int Depth { get; set; }

        public List<InlineStyleRange> InlineStyleRanges { get; set; }

        public List<EntityRange> EntityRanges { get; set; }

        public TreeNode ConvertToTreeNode(Dictionary<int, Entity> entityMap, int prevDepth)
        {
            var node = new TreeNode(Type, TreeNodeType.Block, Text, Depth, prevDepth);
            
            if (InlineStyleRanges.Count == 0 && EntityRanges.Count == 0)
            {
                return node;
            }
            
            var indexesSet = new SortedSet<int>
            {
                0, Text.Length
            };
            var ranges = InlineStyleRanges.Cast<IHasOffsetLength>().Concat(EntityRanges);

            foreach (var range in ranges)
            {
                indexesSet.Add(range.Offset);
                indexesSet.Add(range.Offset + range.Length);
            }

            var indexes = indexesSet.ToList();
            
            TreeNode openedEntity = null;
            int? openedEntityStopIndex = null;

            for (var i = 0; i < indexes.Count - 1; i++)
            {
                var index = indexes[i];
                var nextIndex = indexes[i + 1];
                var text = Text.Substring(index, nextIndex - index);
                TreeNode child = null;

                foreach (var styleRange in InlineStyleRanges)
                {
                    if (index >= styleRange.Offset && nextIndex <= styleRange.Offset + styleRange.Length)
                    { 
                        if (child == null)
                        {
                            child = new TreeNode(styleRange.Style, TreeNodeType.Style, text, 0, 0);
                        }
                        else
                        {
                            child.Text = null;
                            child.AppendChild(new TreeNode(styleRange.Style, TreeNodeType.Style, text, 0, 0));
                        }
                    } 
                }
                
                if (child == null)
                {
                    child = new TreeNode(null, TreeNodeType.Block, text);
                }

                if (openedEntity == null)
                {
                    foreach (var entityRange in EntityRanges)
                    {
                        if (index == entityRange.Offset)
                        {
                            var entity = entityMap[entityRange.Key];
                            openedEntity = new TreeNode(entity.Type, TreeNodeType.Entity, null, 0, 
                                0, entity.Data);
                            openedEntityStopIndex = entityRange.Offset + entityRange.Length;
                        }
                    }

                    if (openedEntity == null)
                    {
                        node.AppendChild(child);
                    }
                }
                
                if (openedEntity != null && openedEntityStopIndex != null)
                {
                    if (nextIndex < openedEntityStopIndex)
                    {
                        openedEntity.AppendChild(child);
                    }

                    if (nextIndex == openedEntityStopIndex)
                    {
                        openedEntity.AppendChild(child);
                        node.AppendChild(openedEntity);
                        openedEntity = null;    
                    }
                }
            }

            return node;
        }
    }
}