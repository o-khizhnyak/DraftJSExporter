using System.Collections.Generic;

namespace DraftJSExporter
{
    public class WrapperStack
    {
        public WrapperStack()
        {
            _stack = new List<string>();
        }

        private List<string> _stack;

        public int GetLength()
        {
            return _stack.Count;
        }
    }
}