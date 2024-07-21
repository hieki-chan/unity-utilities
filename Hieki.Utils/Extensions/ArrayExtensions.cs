using UnityEngine;

namespace Hieki.Utils
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Pick one random element.
        /// </summary>
        public static T PickOne<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
                return default;

            return array[Random.Range(0, array.Length)];
        }

        // <summary>
        /// Swaps two elements in the array at the specified indices.
        /// </summary>
        public static T[] SwapAt<T>(this T[] source, int index1, int index2)
        {
            if (index1 > source.Length - 1 || index2 > source.Length - 1 || index1 < 0 || index2 < 0)
            {
                Debug.LogWarning("Swaping Array: Index is out of range");
                return source;
                //throw new IndexOutOfRangeException("Swaping Array: Index is out of range");
            }

            T val = source[index1];
            source[index1] = source[index2];
            source[index2] = val;

            return source;
        }

        /// <summary>
        /// Shuffle the <see cref="Array{T}"/> in place.
        /// </summary>
        public static T[] ShuffleSelf<T>(this T[] array)
        {
            int count = array.Length;

            for (int i = 0; i < count / 2 + 1; i++)
            {
                array.SwapAt(Random.Range(0, count), Random.Range(0, count));
            }

            return array;
        }

        /// <summary>
        /// Shuffle the <see cref="Array{T}"/> in place.
        /// </summary>
        public static T[] ShuffleSelf<T>(this T[] array, int start, int end)
        {
            if (start < 0 || end < 0 || start >= array.Length || end >= array.Length)
            {
                return array;
            }
            for (int i = start; i < end / 2 + 1; i++)
            {
                array.SwapAt(Random.Range(start, end), Random.Range(start, end));
            }

            return array;
        }

        /// <summary>
        /// Sorts the list of components by their distance to the specified position.
        /// </summary>
        public static void SortedByDistanceTo<T>(this T[] source, Vector3 position) where T : Component
        {
            int count = source.Length;
            for (int i = 0; i < count - 1; i++)
            {
                int current_min_j = i;
                float distance_i = (position - source[i].transform.position).sqrMagnitude;

                for (int j = i + 1; j < count; j++)
                {
                    float distance_j = (position - source[j].transform.position).sqrMagnitude;

                    if (distance_j < distance_i)
                    {
                        current_min_j = j;
                    }
                }

                (source[current_min_j], source[i]) = (source[i], source[current_min_j]);
            }
        }
    }
}
