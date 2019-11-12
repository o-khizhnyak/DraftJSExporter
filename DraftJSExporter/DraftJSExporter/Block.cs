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

        public Element ConvertToElement(ExporterConfig config, Dictionary<int, Entity> entityMap, Element wrapper, 
            int prevDepth)
        {
            if (InlineStyleRanges.Count == 0 && EntityRanges.Count == 0)
            {
                return config.BlockMap.GenerateBlockElement(Type, Depth, prevDepth, Text, wrapper);
            }
            
            var element = config.BlockMap.GenerateBlockElement(Type, Depth, prevDepth, null, wrapper);

            var container = element.Wrapper ? element.Children.Last() : element;
            
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
            
            Element openedEntity = null;
            int? openedEntityStopIndex = null;

            for (var i = 0; i < indexes.Count - 1; i++)
            {
                var index = indexes[i];
                var nextIndex = indexes[i + 1];
                var text = Text.Substring(index, nextIndex - index);
                Element child = null;

                foreach (var styleRange in InlineStyleRanges)
                {
                    if (index >= styleRange.Offset && nextIndex <= styleRange.Offset + styleRange.Length)
                    { 
                        if (child == null)
                        {
                            child = new Element(config.StyleMap[styleRange.Style], null, text, true);                            
                        }
                        else
                        {
                            child.Text = null;
                            child.AppendChild(new Element(config.StyleMap[styleRange.Style], null, text, true));
                        }
                    } 
                }
                
                if (child == null)
                {
                    child = new Element(null, null, text, true);
                }

                if (openedEntity == null)
                {
                    foreach (var entityRange in EntityRanges)
                    {
                        if (index == entityRange.Offset)
                        {
                            var entity = entityMap[entityRange.Key];
                            openedEntity = config.EntityDecorators[entity.Type](entity.Data);
                            openedEntityStopIndex = entityRange.Offset + entityRange.Length;
                        }
                    }

                    if (openedEntity == null)
                    {
                        container.AppendChild(child);
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
                        container.AppendChild(openedEntity);
                        openedEntity = null;    
                    }
                }
            }

            return element;
        }
    }
}