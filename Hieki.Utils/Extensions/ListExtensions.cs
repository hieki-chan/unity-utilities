using System.Collections.Generic;
using UnityEngine;

namespace Hieki.Utils
{
    public static class ListExtensions
    {
        /// <summary>
        /// Swaps two elements in the list at the specified indices.
        /// </summary>
        public static void SwapAt<T>(this List<T> source, int index1, int index2)
        {
            if (index1 >= source.Count || index2 >= source.Count || index1 < 0 || index2 < 0)
            {
                Debug.Log("Swaping List: Index is out of range");
                return;
            }

            T val = source[index1];
            source[index1] = source[index2];
            source[index2] = val;
        }

        /// <summary>
        /// Removes the element at the specified index and swaps the last element into its place. 
        /// This is more efficient than moving all elements following the removed element, but does change the order of elements in the buffer.
        /// </summary>
        public static List<T> RemoveAtSwapBack<T>(this List<T> source, int index)
        {
            if (source == null || index < 0 || index > source.Count)
            {
                return source;
            }

            int count = source.Count;
            source[index] = source[count - 1];
            source.RemoveAt(count - 1);

            return source;
        }

        /// <summary>
        /// Removes the element with the specified value and swaps the last element into its place. 
        /// This is more efficient than moving all elements following the removed element, but does change the order of elements in the buffer.
        /// </summary>
        public static List<T> RemoveSwapBack<T>(this List<T> source, T value)
        {
            int index = source.IndexOf(value);
            return RemoveAtSwapBack(source, index);
        }

        /// <summary>
        /// Move element at given index to the end of the list.
        /// </summary>
        public static void SetAsLastIndex<T>(this List<T> source, int index)
        {
            if (index >= source.Count || index < 0)
            {
                Debug.Log("Swaping List: Index is out of range");
                return;
            }
            var value = source[index];

            source.RemoveAt(index);
            source.Add(value);
        }

        public static T PickOne<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                return default;

            return list[Random.Range(0, list.Count)];
        }

        public static int PickIndex<T>(this List<T> list)
        {
            if (list.Count == 0)
                return -1;
            return Random.Range(0, list.Count);
        }


        /// <summary>
        /// Foreach elements but start at random index and loop it. Ex : 3->4->5->0->1->2
        /// </summary>
        public static List<T> ForeachAtRandomStart<T>(this List<T> list, System.Action<T> callback)
        {
            int count = list.Count;
            int realIndex = Random.Range(0, count);
            int loopIndex = realIndex;

            for (int i = 0; i < count; i++)
            {
                var item = list[realIndex];

                callback?.Invoke(item);

                loopIndex++;
                realIndex = (int)Mathf.Repeat(loopIndex, count - 1);
            }

            return list ;
        }
    }
}
