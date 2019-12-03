using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DraftJs.Abstractions;
using DraftJs.Exporter.Models;

namespace DraftJs.Exporter
{
    public static class ContentStateToTreeConverter
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new IntDictionaryJsonConverter()
            }
        };

        public static DraftJsRootNode Convert(string contentStateJson)
        {
            if (string.IsNullOrWhiteSpace(contentStateJson))
            {
                return null;
            }

            return Convert(JsonSerializer.Deserialize<ContentState>(contentStateJson, JsonSerializerOptions));
        }

        public static DraftJsRootNode Convert(ContentState contentState)
        {
            if (contentState.Blocks == null)
            {
                return null;
            }
            
            var nodes = new List<DraftJsTreeNode>();

            foreach (var block in contentState.Blocks)
            {
                var node = ConvertBlockToTreeNode(block, contentState.EntityMap);
                nodes.Add(node);
            }

            return new DraftJsRootNode(nodes);
        }

        private static DraftJsTreeNode ConvertBlockToTreeNode(Block block, IReadOnlyDictionary<int, Entity> entityMap)
        {
            var blockNode = GetBlockTreeNode(block.Type);
            blockNode.Depth = block.Depth;
            
            if (block.InlineStyleRanges.Count == 0 && block.EntityRanges.Count == 0)
            {
                blockNode.Text = block.Text;
                return blockNode;
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
            
            DraftJsTreeNode openedEntity = null;
            int? openedEntityStopIndex = null;

            for (var i = 0; i < indexes.Count - 1; i++)
            {
                var index = indexes[i];
                var nextIndex = indexes[i + 1];
                var text = block.Text.Substring(index, nextIndex - index);
                DraftJsTreeNode child = null;

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
                
                if (openedEntity == null)
                {
                    foreach (var entityRange in block.EntityRanges)
                    {
                        if (index == entityRange.Offset)
                        {
                            var entity = entityMap[entityRange.Key];
                            openedEntity = new EntityTreeNode(entity.Type, entity.Data);
                            openedEntityStopIndex = entityRange.Offset + entityRange.Length;
                        }
                    }

                    if (openedEntity == null)
                    {
                        blockNode.AppendChild(child ?? new TextTreeNode(text));
                    }
                }
                
                if (openedEntity != null && openedEntityStopIndex != null)
                {
                    if (nextIndex < openedEntityStopIndex || nextIndex == openedEntityStopIndex)
                    {
                        if (child == null)
                        {
                            if (openedEntity.Children.Count == 0)
                            {
                                openedEntity.Text = text;
                            }
                            else
                            {
                                openedEntity.AppendChild(new TextTreeNode(text));
                            }
                        }
                        else
                        {
                            openedEntity.AppendChild(child);
                        }
                    }

                    if (nextIndex == openedEntityStopIndex)
                    {
                        blockNode.AppendChild(openedEntity);
                        openedEntity = null;    
                    }
                }
            }

            return blockNode;
        }

        private static BlockTreeNode GetBlockTreeNode(string type)
        {
            switch (type)
            {
                case "unstyled":
                    return new UnstyledBlock();
                case "header-one":
                    return new HeaderOneBlock();
                case "header-two":
                    return new HeaderTwoBlock();
                case "header-three":
                    return new HeaderThreeBlock();
                case "header-four":
                    return new HeaderFourBlock();
                case "header-five":
                    return new HeaderFiveBlock();
                case "header-six":
                    return new HeaderSixBlock();
                case "unordered-list-item":
                    return new UnorderedListItemBlock();
                case "ordered-list-item":
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

        private static StyleTreeNode GetStyleTreeNode(string type)
        {
            switch (type)
            {
                case "BOLD":
                    return new BoldStyleTreeNode();
                case "CODE":
                    return new CodeStyleTreeNode();
                case "ITALIC":
                    return new ItalicStyleTreeNode();
                case "UNDERLINE":
                    return new UnderlineStyleTreeNode();
                case "STRIKETHROUGH":
                    return new StrikethroughStyleTreeNode();
                case "SUPERSCRIPT":
                    return new SuperscriptStyleTreeNode();
                case "SUBSCRIPT":
                    return new SubscriptStyleTreeNode();
                case "MARK":
                    return new MarkStyleTreeNode();
                case "QUOTATION":
                    return new QuotationStyleTreeNode();
                case "SMALL":
                    return new SmallStyleTreeNode();
                case "SAMPLE":
                    return new SampleStyleTreeNode();
                case "INSERT":
                    return new InsertStyleTreeNode();
                case "DELETE":
                    return new DeleteStyleTreeNode();
                case "KEYBOARD":
                    return new KeyboardStyleTreeNode();
                default:
                    throw new Exception($"Unknown style type: {type}"); 
            }
        }
    }
}