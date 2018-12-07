using System.Collections.Generic;
using Newtonsoft.Json;

namespace DraftJSExporter
{
    public class Exporter
    {
        public Exporter(ExporterConfig config)
        {
            _config = config;
        }

        private ExporterConfig _config;
        
        public string Render(string contentStateJson)
        {
            if (contentStateJson == null)
            {
                return "";
            }

            var contentState = JsonConvert.DeserializeObject<ContentState>(contentStateJson);
            var wrapperState = new WrapperState(contentState.Blocks);
            var document = new Element();
            var minDepth = 0;

            foreach (var block in contentState.Blocks)
            {
                var element = RenderBlock(block, contentState.EntityMap, wrapperState);
                
                if (block.Depth > minDepth)
                {
                    minDepth = block.Depth;
                }

                if (block.Depth == 0)
                {
                    document.AppendChild(element);
                }
            }

            if (minDepth > 0 && wrapperState.GetStackLength() != 0)
            {
                document.AppendChild(wrapperState.GetStackTail());
            }

            return document.Render();
        }

        private Element RenderBlock(Block block, Dictionary<int, Entity> entityMap, WrapperState wrapperState)
        {
            var content = block.ConvertToElement(_config.StyleMap, entityMap);
            return content;
        }
    }
}