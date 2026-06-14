using System;
using System.Collections.Generic;

namespace oneBlack74.UnityToolbox.Core
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
            => list == null || list.Count == 0;

        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            if (list.IsNullOrEmpty())
                throw new InvalidOperationException("List is null or empty.");

            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}
