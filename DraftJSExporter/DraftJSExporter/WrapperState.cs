using System.Collections.Generic;

namespace DraftJSExporter
{
    public class WrapperState
    {
        public WrapperState(List<Block> blocks)
        {
            _wrapperStack = new WrapperStack();
            _blocks = blocks;
        }

        private WrapperStack _wrapperStack;
        private List<Block> _blocks;

        public int GetStackLength()
        {
            return _wrapperStack.GetLength();
        }

        public Element GetStackTail()
        {
            return new Element(null, null);
        }
    }
}