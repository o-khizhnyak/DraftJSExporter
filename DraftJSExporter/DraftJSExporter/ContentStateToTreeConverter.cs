using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DraftJSExporter
{
    public class ContentStateToTreeConverter
    {
        public DraftJSRootNode Convert(string contentStateJson)
        {
            if (contentStateJson == null)
            {
                return null;
            }

            var contentState = JsonConvert.DeserializeObject<ContentState>(contentStateJson);

            if (contentState.Blocks == null)
            {
                return null;
            }
            
            var nodes = new List<DraftJSTreeNode>();
            var prevDepth = -1;

            foreach (var block in contentState.Blocks)
            {
                var node = ConvertBlockToTreeNode(block, contentState.EntityMap, prevDepth);
                prevDepth = block.Depth;
                nodes.Add(node);
            }

            return new DraftJSRootNode(nodes);
        }

        private DraftJSTreeNode ConvertBlockToTreeNode(Block block, IReadOnlyDictionary<int, Entity> entityMap, int prevDepth)
        {
            var node = GetBlockTreeNode(block.Type);
            node.Text = block.Text;
            
            if (block.InlineStyleRanges.Count == 0 && block.EntityRanges.Count == 0)
            {
                return node;
            }
            
            var indexesSet = new SortedSet<int>
            {
                0, block.Text.Length
            };
            var ranges = block.InlineStyleRanges.Cast<IHasOffsetLength>().Concat(block.EntityRanges);

            foreach (var range in ranges)
            {
                indexesSet.Add(range.Offset);
                indexesSet.Add(range.Offset + range.Length);
            }

            var indexes = indexesSet.ToList();
            
            DraftJSTreeNode openedEntity = null;
            int? openedEntityStopIndex = null;

            for (var i = 0; i < indexes.Count - 1; i++)
            {
                var index = indexes[i];
                var nextIndex = indexes[i + 1];
                var text = block.Text.Substring(index, nextIndex - index);
                DraftJSTreeNode child = null;

                foreach (var styleRange in block.InlineStyleRanges)
                {
                    if (index >= styleRange.Offset && nextIndex <= styleRange.Offset + styleRange.Length)
                    { 
                        var styleTreeNode = GetStyleTreeNode(styleRange.Style);
                        styleTreeNode.Text = text;
                            
                        if (child == null)
                        {
                            child = styleTreeNode;
                        }
                        else
                        {
                            child.Text = null;
                            child.AppendChild(styleTreeNode);
                        }
                    } 
                }
                
                if (child == null)
                {
                    child = new TextTreeNode(text);
                }

                if (openedEntity == null)
                {
                    foreach (var entityRange in block.EntityRanges)
                    {
                        if (index == entityRange.Offset)
                        {
                            var entity = entityMap[entityRange.Key];
                            openedEntity = new EntityTreeNode();
                            
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

        private BlockTreeNode GetBlockTreeNode(string type)
        {
            switch (type)
            {
                case "unstyled":
                    return new UnstyledBlock();
                case "headerOne":
                    return new HeaderOneBlock();
                case "headerTwo":
                    return new HeaderTwoBlock();
                case "headerThree":
                    return new HeaderThreeBlock();
                case "headerFour":
                    return new HeaderFourBlock();
                case "headerFive":
                    return new HeaderFiveBlock();
                case "headerSix":
                    return new HeaderSixBlock();
                case "unorderedListItem":
                    return new UnorderedListItemBlock();
                case "orderedListItem":
                    return new OrderedListItemBlock();
                case "blockquote":
                    return new BlockquoteBlock();
                case "pre":
                    return new PreBlock();
                case "atomic":
                    return new AtomicBlock();
                default:
                    throw new Exception($"Unknown block type: {type}");
            }
        }

        private StyleTreeNode GetStyleTreeNode(string type)
        {
            switch (type)
            {
                case "bold":
                    return new BoldStyleTreeNode();
                case "code":
                    return new CodeStyleTreeNode();
                case "italic":
                    return new ItalicStyleTreeNode();
                case "underline":
                    return new UnderlineStyleTreeNode();
                case "strikethrough":
                    return new StrikethroughStyleTreeNode();
                case "superscript":
                    return new SuperscriptStyleTreeNode();
                case "subscript":
                    return new SubscriptStyleTreeNode();
                case "mark":
                    return new MarkStyleTreeNode();
                case "quotation":
                    return new QuotationStyleTreeNode();
                case "small":
                    return new SmallStyleTreeNode();
                case "sample":
                    return new SampleStyleTreeNode();
                case "insert":
                    return new InsertStyleTreeNode();
                case "delete":
                    return new DeleteStyleTreeNode();
                case "keyboard":
                    return new KeyboardStyleTreeNode();
                default:
                    throw new Exception($"Unknown style type: {type}");
            }
        }
    }
}