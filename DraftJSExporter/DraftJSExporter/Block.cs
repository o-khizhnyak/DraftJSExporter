using System.Collections.Generic;
using System.Linq;
using DraftJSExporter.Defaults;
using Newtonsoft.Json;

namespace DraftJSExporter
{
    public class Block
    {
        public string Key { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }
        
        public int Depth { get; set; }

        public List<InlineStyleRange> InlineStyleRanges { get; set; }

        public List<EntityRange> EntityRanges { get; set; }

        public Element ConvertToElement(StyleMap styleMap, Dictionary<int, Entity> entityMap)
        {
            if (InlineStyleRanges.Count == 0 && EntityRanges.Count == 0)
            {
                return new Element(Type, new Dictionary<string, string>(), Text);
            }
            
            var element = new Element(Type);
            
            var indexes = InlineStyleRanges.Select(x => x.Offset)
                .Union(InlineStyleRanges.Select(x => x.Offset + x.Length))
                .Union(EntityRanges.Select(x => x.Offset))
                .Union(EntityRanges.Select(x => x.Offset + x.Length))
                .Append(0)
                .Append(Text.Length)
                .Distinct()
                .ToList();

            Element openedEntity = null;
            int? openedEntityStopIndex = null;

            for (var i = 0; i < indexes.Count - 1; i++)
            {
                var index = indexes[i];
                var nextIndex = indexes[i + 1];
                var text = Text.Substring(index, nextIndex);
                Element child = null;

                foreach (var styleRange in InlineStyleRanges)
                {
                    if (index >= styleRange.Offset && nextIndex <= styleRange.Offset + styleRange.Length)
                    { 
                        if (child == null)
                        {
                            child = new Element(styleMap[styleRange.Style], null, text, true);                            
                        }
                        else
                        {
                            child.AppendChild(new Element(styleMap[styleRange.Style], null, text, true));
                        }
                    } 
                }
                
                if (child == null)
                {
                    child = new Element(null, null, text, true);
                }

                if (openedEntity != null && openedEntityStopIndex != null && nextIndex < openedEntityStopIndex)
                {
                    openedEntity.AppendChild(child);
                }
                
                if (openedEntity != null && openedEntityStopIndex != null && nextIndex == openedEntityStopIndex)
                {
                    openedEntity.AppendChild(child);
                    element.AppendChild(openedEntity);
                    openedEntity = null;
                }

                if (openedEntity == null)
                {
                    foreach (var entityRange in EntityRanges)
                    {
                        if (index == entityRange.Offset)
                        {
                            var entity = entityMap[entityRange.Key];
                            openedEntity = new Element(entity.Type);
                            openedEntity.AppendChild(child);
                            openedEntityStopIndex = entityRange.Offset + entityRange.Length;
                        }
                    }
                }
            }

            return element;
        }
    }
}