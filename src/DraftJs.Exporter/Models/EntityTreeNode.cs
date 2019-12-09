using System.Collections.Generic;

namespace DraftJs.Exporter.Models
{
    public class EntityTreeNode : DraftJsTreeNode
    {
        public EntityTreeNode(string type, IReadOnlyDictionary<string, object> data)
        {
            Type = type;
            Data = data;
        }

        public string Type { get; set; }
        public IReadOnlyDictionary<string, object> Data { get; }
    }
}