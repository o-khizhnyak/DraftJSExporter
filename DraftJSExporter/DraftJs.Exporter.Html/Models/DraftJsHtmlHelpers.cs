using System;
using System.Collections.Generic;
using System.Text.Json;
using HtmlTags;

namespace DraftJs.Exporter.Html.Models
{
    public static class DraftJsHtmlHelpers
    {
        public static HtmlTag ConfigureAttributesFromEntityData(this HtmlTag tag, IReadOnlyDictionary<string, object> entityData)
        {
            void Configure(object value, string key)
            {
                switch (value)
                {
                    case string strValue:
                        tag.Attr(key, strValue);
                        break;
                    case JsonElement jsonVal:
                    {
                        var attrValue = jsonVal.ValueKind switch
                        {
                            JsonValueKind.Undefined => null,
                            JsonValueKind.Object => null,
                            JsonValueKind.Array => null,
                            JsonValueKind.String => jsonVal.GetString(),
                            JsonValueKind.Number => (jsonVal.TryGetDouble(out var doubleVal) ? doubleVal : (object) null),
                            JsonValueKind.True => string.Empty,
                            JsonValueKind.False => null,
                            JsonValueKind.Null => null,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        if (attrValue != null)
                        {
                            tag.Attr(key, attrValue);
                        }
                        break;
                    }
                }
            }

            foreach (var (key, value) in entityData)
            {
                Configure(value, key);
            }

            return tag;
        }
    }
}