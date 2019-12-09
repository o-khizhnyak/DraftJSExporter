using System;

namespace DraftJs.Exporter.Models
{
    public abstract class StyleTreeNode: DraftJsTreeNode
    {
        public static StyleTreeNode Create(string type)
        {
            return type switch
            {
                "BOLD" => (StyleTreeNode) new BoldStyleTreeNode(),
                "CODE" => new CodeStyleTreeNode(),
                "ITALIC" => new ItalicStyleTreeNode(),
                "UNDERLINE" => new UnderlineStyleTreeNode(),
                "STRIKETHROUGH" => new StrikethroughStyleTreeNode(),
                "SUPERSCRIPT" => new SuperscriptStyleTreeNode(),
                "SUBSCRIPT" => new SubscriptStyleTreeNode(),
                "MARK" => new MarkStyleTreeNode(),
                "QUOTATION" => new QuotationStyleTreeNode(),
                "SMALL" => new SmallStyleTreeNode(),
                "SAMPLE" => new SampleStyleTreeNode(),
                "INSERT" => new InsertStyleTreeNode(),
                "DELETE" => new DeleteStyleTreeNode(),
                "KEYBOARD" => new KeyboardStyleTreeNode(),
                _ => throw new Exception($"Unknown style type: {type}")
            };
        }
    }
}