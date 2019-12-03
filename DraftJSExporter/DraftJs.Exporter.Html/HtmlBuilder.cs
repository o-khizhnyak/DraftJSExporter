using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftJs.Exporter.Html
{
    public class HtmlBuilder
    {
        private const int TabWidth = 4;
        private readonly StringBuilder _sb;
        private int _level;
        private bool _addLine;
        
        public HtmlBuilder()
        {
            _sb = new StringBuilder();
            _level = 0;
            _addLine = false;
        }

        public void AddOpeningTag(string tagName, IReadOnlyDictionary<string, string> attributes, bool inline, 
            bool selfClosing)
        {
            if (tagName != null)
            {
                if (_addLine)
                {
                    _sb.AppendLine();
                    _sb.Append(new string(' ', TabWidth * _level));
                    _addLine = false;
                }
                
                var attr = attributes == null ? "" : string.Join("", attributes.Select(a => new StringBuilder(" ")
                    .AppendFormat("{0}=\"{1}\"", a.Key, a.Value).ToString()).ToArray());
                
                _sb.AppendFormat("<{0}{1}", tagName, attr);

                if (!selfClosing)
                {
                    _sb.Append(">");
                    
                    if (!inline)
                    {
                        _level++;
                        _sb.AppendLine();
                        _sb.Append(new string(' ', TabWidth * _level));
                    }
                }
            }
        }

        public void AddClosingTag(string tagName, bool inline)
        {
            if (tagName != null)
            {
                if (!inline)
                {
                    _level--;
                    _sb.AppendLine();
                    _sb.Append(new string(' ', TabWidth * _level));
                    _addLine = true;
                }
                
                _sb.AppendFormat("</{0}>", tagName);
            }
        }

        public void AddText(string text)
        {
            if (text == null)
            {
                return;
            }
            
            _sb.Append(text);
        }

        public void CloseTag(bool inline)
        {
            if (inline)
            {
                _sb.Append(" /> ");    
            }
            else
            {
                _sb.Append(" />");
                _sb.AppendLine();
            }
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}