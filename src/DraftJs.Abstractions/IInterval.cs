namespace DraftJs.Abstractions
{
    public interface IInterval
    {
        int From { get; }

        int To { get;  }
    }

    internal static class HasOffsetLengthExtensions
    {
        public static bool HasIntersection(this IInterval entity, IInterval other)
        {
            return entity.To >= other.From && entity.To <= other.To
                || entity.From >= other.From && entity.From <= other.To;
        }
    }
}