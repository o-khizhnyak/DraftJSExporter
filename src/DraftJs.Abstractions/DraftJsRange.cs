using System.Collections.Generic;
using JetBrains.Annotations;

namespace DraftJs.Abstractions
{
    public class DraftJsRange
    {
        public DraftJsRange(int start, int end, int? entityKey, IReadOnlyCollection<string> styles)
        {
            Start = start;
            End = end;
            EntityKey = entityKey;
            Styles = styles;
        }

        public int Start { get; }

        public int End { get; }

        public int? EntityKey { get; }

        [CanBeNull]
        public IReadOnlyCollection<string> Styles { get; }
    }
}