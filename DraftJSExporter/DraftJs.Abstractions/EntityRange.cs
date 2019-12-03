namespace DraftJs.Abstractions
{
    public class EntityRange: IHasOffsetLength
    {
        public int Offset { get; set; }

        public int Length { get; set; }

        public int Key { get; set; }
    }
}