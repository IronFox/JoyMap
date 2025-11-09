namespace JoyMap.Extensions
{
    public static class ListExtensions
    {
        public static int IndexOf<T>(this IReadOnlyList<T> list, Predicate<T> match)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (match(list[i]))
                    return i;
            }
            return -1;
        }
    }
}
