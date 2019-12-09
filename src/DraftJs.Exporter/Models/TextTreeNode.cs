namespace DraftJs.Exporter.Models
{
    public class TextTreeNode : DraftJsTreeNode
    {
        public TextTreeNode(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}