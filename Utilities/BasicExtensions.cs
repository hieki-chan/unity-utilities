using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityRandom = UnityEngine.Random;

namespace Utilities
{
    public static class BasicExtensions
    {
        #region Text
        public static void CopyToClipboard<T>(this T text) where T : TMP_Text
        {
            TextEditor textEditor = new TextEditor();
            textEditor.text = text.text;
            textEditor.SelectAll();
            textEditor.Copy();
        }

        public static void CopyToClipboard(this string text)
        {
            TextEditor textEditor = new TextEditor();
            textEditor.text = text;
            textEditor.SelectAll();
            textEditor.Copy();
        }

        public static string GetMinsAndSecs(this float value)
        {
            if (value < 0)
                return "00:00";
            TimeSpan time = TimeSpan.FromSeconds(value);
            string z1 = time.Minutes < 10 ? "0" : "";
            string z2 = time.Seconds < 10 ? "0" : "";
            return z1 + time.Minutes + ":" + z2 + time.Seconds;
        }

        #endregion

        #region Array
        public static T[] Append<T>(this T[] array, T item)
        {
            if (array == null)
            {
                return new T[] { item };
            }
            T[] result = new T[array.Length + 1];
            array.CopyTo(result, 0);
            result[array.Length] = item;
            return result;
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            if (source.Length <= 0)
                return source;
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }
        public static T[] Swap<T>(this T[] source, int index1, int index2)
        {
            if (index1 > source.Length - 1 || index2 > source.Length - 1 || index1 < 0 || index2 < 0)
            {
                Debug.Log("Swaping Array: Index is out of range");
                return source;
            }
            var copy = new T[source.Length];
            Array.Copy(source, copy, source.Length);

            T val = copy[index1];
            copy[index1] = copy[index2];
            copy[index2] = val;

            return copy;
        }
        #endregion

        #region List
        public static List<T> Swap<T>(this List<T> source, int index1, int index2)
        {
            if (index1 > source.Count - 1 || index2 > source.Count - 1 || index1 < 0 || index2 < 0)
            {
                Debug.Log("Swaping List: Index is out of range");
                return source;
            }
            var copy = new List<T>(source);

            T val = copy[index1];   
            copy[index1] = copy[index2];
            copy[index2] = val;

            return copy;
        }
        public static List<T> SetAsLastIndex<T>(this List<T> source, int index)
        {
            if (index > source.Count - 1 || index < 0)
            {
                Debug.Log("Swaping List: Index is out of range");
                return source;
            }
            var copy = new List<T>(source);

            copy.RemoveAt(index);
            copy.Add(source[index]);

            return copy;
        }
        #endregion

        #region Color
        public static string ColorToHtml(this Color color)
        {
            return "#" + ColorUtility.ToHtmlStringRGB(color);
        }
        #endregion

        #region Convert
        public static bool ToBool(this int intValue)
        {
            return intValue == 1 ? true : intValue == 0 ? false : false;
        }
        public static bool ToBool(this string stringValue)
        {
            return stringValue == "true" ? true : stringValue == "false" ? false : false;
        }

        public static int ToInt(this bool boolValue)
        {
            return boolValue ? 1 : 0;
        }
        public static int ToInt(this string stringValue)
        {
            bool result = int.TryParse(stringValue, out int intValue);
            if (result)
                return intValue;
            else
            {
                Debug.Log("failed to convert string to int, return 0");
                return 0;
            }
        }
        public static float ToFloat(this string stringValue)
        {
            bool result = float.TryParse(stringValue, out float floatValue);
            if (result)
                return floatValue;
            else
            {
                Debug.Log("failed to convert string to float, return 0.0f");
                return 0.0f;
            }
        }
        #endregion

        #region Vector
        public static Vector3Int GetVector3Int(this Vector3 vector)
        {
            return new Vector3Int((int)vector.x, (int)vector.y, (int)vector.z);
        }
        public static Vector3 GetNoise(this Vector3 source, float strength)
        {
            return new Vector3(source.x + UnityRandom.Range(-1.0f, 1.0f) * strength, 
                source.y + UnityRandom.Range(-1.0f, 1.0f) * strength,
                source.z + UnityRandom.Range(-1.0f, 1.0f) * strength);
        }
        public static Vector2 GetNoise(this Vector2 source, float strength)
        {
            var _noise = Mathf.PerlinNoise(source.x * strength, source.y * strength);
            return new Vector2(_noise, _noise);
        }
        public static Vector3 ToFlat(this Vector3 source)
        {
            return new Vector3(source.x, 0, source.z);
        }
        #endregion

        #region GameObject
        public static T GetComponentInChildrenExcludeParent<T>(this GameObject obj, bool includeInactive = false) where T : Component
        {
            var components = obj.GetComponentsInChildren<T>(includeInactive);

            return components.FirstOrDefault(childComponent =>
                childComponent.transform != obj.transform);
        }
        #endregion

        #region Monobehaviour

        public static void SetActive(this MonoBehaviour monoBehaviour, bool value)
        {
            monoBehaviour.gameObject.SetActive(value);
        }
        public static void LookAtTarget(this MonoBehaviour monoBehaviour, Vector3 targetPosition, float speed = 5.0f)
        {
            monoBehaviour.transform.LookAtTarget(targetPosition, speed);
        }
        #endregion

        #region Transform 
        public static T[] GetChilds<T>(this Transform parent) where T : Component
        {
            int count = parent.childCount;
            T[] result = new T[0];
            for (int i = 0; i < count; i++)
            {
                var t = parent.GetChild(i).GetComponent<T>();
                if (t)
                    result = result.Append(t);
            }
            return result;
        }
        public static Transform[] GetChilds(this Transform parent)
        {
            int count = parent.childCount;
            Transform[] result = new Transform[0];
            for (int i = 0; i < count; i++)
            {
                result = result.Append(parent.GetChild(i));
            }
            return result;
        }
        public static void LookAtTarget(this Transform transform, Vector3 targetPosition, float speed = 5.0f)
        {
            Vector3 direction = (targetPosition - transform.position).ToFlat();
            if (direction == Vector3.zero)
                return;
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                lookRotation, Time.deltaTime * speed);
        }
        #endregion

        #region Scripableobject
        public static T Clone<T>(this T scrtipableObject) where T : ScriptableObject
        {
            if (scrtipableObject == null)
            {
                return (T)ScriptableObject.CreateInstance(typeof(T));
            }

            T instance = Object.Instantiate(scrtipableObject);
            instance.name = scrtipableObject.name;
            return instance;
        }
        #endregion
    }
}
