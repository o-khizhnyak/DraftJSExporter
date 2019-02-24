using System.Collections.Generic;
using System.Linq;
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
            var root = new Element();
            Element wrapper = null;
            var prevDepth = -1;

            foreach (var block in contentState.Blocks)
            {
                var element = block.ConvertToElement(_config, contentState.EntityMap, wrapper, prevDepth);
                prevDepth = block.Depth;
                if (element.Wrapper)
                {
                    if (wrapper == null)
                    {
                        wrapper = element;
                        root.AppendChild(element);
                    }
                }
                else
                {
                    root.AppendChild(element);
                    wrapper = null;
                }
            }

            return root.Render();
        }
    }
}