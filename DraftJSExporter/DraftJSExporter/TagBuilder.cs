using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftJSExporter
{
    public static class TagBuilder
    {
        private const int TabWidth = 4;

        public static void AddOpeningTag(StringBuilder sb, string type, Dictionary<string, string> attributes, int level)
        {
            var attr = string.Join("", attributes.Select(a => new StringBuilder(" ")
                .AppendFormat("{0}=\"{1}\"", a.Key, a.Value).ToString()).ToArray());

            sb.Append(new string(' ', TabWidth * level)).AppendFormat("<{0}{1}>", type, attr).AppendLine();
        }

        public static void AddClosingTag(StringBuilder sb, string type, int level)
        {
            sb.Append(new string(' ', TabWidth * level)).AppendFormat("</{0}>", type);
            if (level != 0)
            {
                sb.AppendLine();
            }
        }
    }
}