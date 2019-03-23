using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftJSExporter
{
    public static class TagBuilder
    {
        private const int TabWidth = 4;

        public static void AddOpeningTag(StringBuilder sb, string type, IReadOnlyDictionary<string, string> attributes, 
            int level, bool inline, bool addTab, bool selfClosing)
        {
            if (addTab)
            {
                sb.Append(new string(' ', TabWidth * level));                
            }

            if (type != null)
            {
                var attr = string.Join("", attributes.Select(a => new StringBuilder(" ")
                    .AppendFormat("{0}=\"{1}\"", a.Key, a.Value).ToString()).ToArray());
                
                sb.AppendFormat("<{0}{1}", type, attr);

                if (!selfClosing)
                {
                    sb.Append(">");
                }
                
                if (!inline && !selfClosing)
                {
                    sb.AppendLine();
                }
            }
        }

        public static void AddClosingTag(StringBuilder sb, string type, int level, bool inline, bool lastChild, 
            bool parentIsInline)
        {
            if (type != null)
            {
                if (!inline)
                {
                    sb.Append(new string(' ', TabWidth * level));
                }
                
                sb.AppendFormat("</{0}>", type);   
            }
            
            if ((!inline || lastChild) && !(!inline && type == null) && !(lastChild && level == 0) && !parentIsInline)
            {
                sb.AppendLine();
            }
        }

        public static void AddText(StringBuilder sb, string text, bool inline, int level)
        {
            if (!inline)
            {
                sb.Append(new string(' ', TabWidth * (level + 1)));
            }
            
            sb.Append(text);
            
            if (!inline)
            {
                sb.AppendLine();
            }
        }

        public static void CloseTag(StringBuilder sb, bool parentIsLastChild)
        {
            sb.Append(" />");
            if (!parentIsLastChild)
            {
                sb.AppendLine();                
            }
        }
    }
}