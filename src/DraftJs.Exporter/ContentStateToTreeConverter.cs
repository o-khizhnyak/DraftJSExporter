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
            return !string.IsNullOrWhiteSpace(contentStateJson) 
                ? Convert(JsonSerializer.Deserialize<ContentState>(contentStateJson, JsonSerializerOptions)) 
                : null;
        }

        public static DraftJsRootNode Convert(ContentState contentState)
        {
            if (contentState.Blocks == null)
            {
                return null;
            }

            var nodes = contentState.Blocks.Select(block => ConvertBlockToTreeNode(block, contentState.EntityMap)).ToList();

            return new DraftJsRootNode(nodes);
        }

        private static DraftJsTreeNode ConvertBlockToTreeNode(Block block, IReadOnlyDictionary<int, Entity> entityMap)
        {
            var blockNode = BlockTreeNode.Create(block.Type, block.Depth);

            if (block.InlineStyleRanges.Count == 0 && block.EntityRanges.Count == 0)
            {
                blockNode.AppendChild(new TextTreeNode(block.Text));
                return blockNode;
            }

            var indexesSet = new SortedSet<int>
            {
                0, block.Text.Length
            };
            var ranges = block.InlineStyleRanges.Cast<IInterval>().Concat(block.EntityRanges);

            foreach (var range in ranges)
            {
                indexesSet.Add(range.From);
                indexesSet.Add(range.To);
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
                        var styleTreeNode = StyleTreeNode.Create(styleRange.Style);
                        styleTreeNode.AppendChild(new TextTreeNode(text));

                        if (child == null)
                        {
                            child = styleTreeNode;
                        }
                        else
                        {
                            child.RemoveLastChild();
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
                                openedEntity.AppendChild(new TextTreeNode(text));
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
    }
}