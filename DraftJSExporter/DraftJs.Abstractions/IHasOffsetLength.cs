namespace DraftJs.Abstractions
{
    public interface IHasOffsetLength
    {
        int Offset { get; set; }

        int Length { get; set; }
    }
}