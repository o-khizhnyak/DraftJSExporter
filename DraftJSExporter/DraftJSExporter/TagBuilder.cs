using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftJSExporter
{
    public static class TagBuilder
    {
        public static string CreateOpeningTag(string type, Dictionary<string, string> attributes)
        {
            var attr = string.Join("", attributes.Select(a => new StringBuilder(" ").Append(a.Key).Append("=")
                .Append(a.Value).ToString()).ToArray());
            
            return new StringBuilder("<").Append(type).Append(attr).Append(">").ToString();
        }

        public static string CrateClosingTag(string type)
        {
            return new StringBuilder("<").Append(type).Append(">").ToString();
        }
    }
}