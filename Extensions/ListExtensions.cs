namespace JoyMap.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns the single element of a sequence, or a default value if the sequence is empty or contains more than
        /// one element.
        /// </summary>
        /// <remarks>Unlike the standard SingleOrDefault method, this method does not throw an exception
        /// if the sequence contains more than one element. Instead, it returns the default value for the type T in that
        /// case.</remarks>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to return a single element from.</param>
        /// <returns>The single element of the input sequence if it contains exactly one element; otherwise, the default value
        /// for type T.</returns>
        public static T? SafeSingleOrDefault<T>(this IEnumerable<T> source)
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                return default;
            var first = enumerator.Current;
            if (!enumerator.MoveNext())
                return first;
            return default;
        }
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
