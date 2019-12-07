using JetBrains.Annotations;

namespace DraftJs.Abstractions
{
    [PublicAPI]
    public class InlineStyleRange: IInterval
    {
        public int Offset { get; set; }

        public int Length { get; set; }

        public string Style { get; set; }
        
        public int From => Offset;
        
        public int To => Offset + Length;
    }
}