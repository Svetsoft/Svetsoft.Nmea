using System;
using System.Collections.Generic;
using System.Linq;

namespace Svetsoft.Nmea.Extensions
{
    internal static class ListExtensions
    {
        /// <summary>
        ///     Determines whether any of specific elements of a sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="TSource"><see cref="System.Collections.Generic.IEnumerable{TSource}" /></typeparam>
        /// <param name="source">
        ///     An <see cref="IEnumerable{TSource}" /> whose elements to apply
        ///     the predicate to.
        /// </param>
        /// <param name="indices">The indices of elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     <bold>true</bold> if any elements with the specified indices in the source sequence pass the test in the specified
        ///     predicate;
        ///     otherwise, <bold>false</bold>.
        /// </returns>
        public static bool Any<TSource>(this IList<TSource> source, IEnumerable<int> indices, Func<TSource, bool> predicate)
        {
            return indices.Any(index => predicate(source[index]));
        }

        /// <summary>
        ///     Determines whether all elements of a specific sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource"><see cref="System.Collections.Generic.IEnumerable" />&lt;<typeparamref name="TSource" />&gt;</typeparam>
        /// <param name="source">
        ///     An <see cref="IEnumerable">&lt;<typeparamref name="TSource" />&gt;</see> whose elements to apply
        ///     the predicate to.
        /// </param>
        /// <param name="indices">The indices of elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     <bold>true</bold> if every element with the specified indices in the source sequence passes the test in the
        ///     specified predicate;
        ///     otherwise, <bold>false</bold>.
        /// </returns>
        public static bool All<TSource>(this IList<TSource> source, IEnumerable<int> indices, Func<TSource, bool> predicate)
        {
            return indices.All(index => predicate(source[index]));
        }
    }
}