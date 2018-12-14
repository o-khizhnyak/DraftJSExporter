namespace DraftJSExporter
{
    public class InlineStyleRange: IHasOffsetLength
    {
        public int Offset { get; set; }

        public int Length { get; set; }

        public string Style { get; set; }
    }
}