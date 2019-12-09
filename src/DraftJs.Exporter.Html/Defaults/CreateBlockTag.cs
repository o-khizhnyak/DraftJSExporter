using DraftJs.Abstractions;
using DraftJs.Exporter.Html.Models;
using HtmlTags;

namespace DraftJs.Exporter.Html.Defaults
{
    /// <summary>Creates <see cref="HtmlTag"/></summary>
    /// <param name="depth">Value of <see cref="Block.Depth"/> from <see cref="ContentState"/></param>
    /// <param name="prevDepth">Previous block depth</param>
    /// <param name="firstChild">Is this first child of its parent</param>
    public delegate HtmlTag CreateBlockTag(int depth, int prevDepth = 0, bool firstChild = false);
}