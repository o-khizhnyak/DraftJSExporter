using Newtonsoft.Json;

namespace DraftJSExporter
{
    public class ContentStateToTreeConverter
    {
        public TreeNode Convert(string contentStateJson)
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

            var root = new TreeNode();
            var prevDepth = -1;

            foreach (var block in contentState.Blocks)
            {
                var node = block.ConvertToTreeNode(contentState.EntityMap, prevDepth);
                prevDepth = block.Depth;
                root.AppendChild(node);
            }

            return root;
        }
    }
}