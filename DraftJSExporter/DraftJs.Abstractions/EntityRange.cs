using JetBrains.Annotations;

namespace DraftJs.Abstractions
{
    [PublicAPI]
    public class EntityRange: IInterval
    {
        public int Offset { get; set; }

        public int Length { get; set; }

        public int Key { get; set; }
        public int From => Offset;
        public int To => Offset + Length;
    }
}