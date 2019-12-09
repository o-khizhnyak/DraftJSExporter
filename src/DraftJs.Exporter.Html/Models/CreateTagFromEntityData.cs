using System.Collections.Generic;
using HtmlTags;

namespace DraftJs.Exporter.Html.Models
{
    public delegate HtmlTag CreateTagFromEntityData(IReadOnlyDictionary<string, object> entityData);
}