using UnityEngine;

namespace Hieki.Utils
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns a <see cref="Vector3"/> that rotates <paramref name="dir"/> around the y axis
        /// </summary>
        public static Vector3 RotateVector(this Vector3 dir, float eulerAnglesY)
        {
            return Quaternion.Euler(0, eulerAnglesY, 0) * dir;
        }

        /// <summary>
        /// Returns a <see cref="Vector3"/> that rotates <paramref name="dir"/> around the y axis from origin
        /// </summary>
        public static Vector3 RotatePosition(this Vector3 pos, float eulerAnglesY, Vector3 origin)
        {
            return Quaternion.Euler(0, eulerAnglesY, 0) * (pos - origin);
        }
        /// <summary>
        /// Get angle in degrees of the <see cref="Vector3"/>
        /// </summary>
        public static float GetAngle(this Vector3 vector)
        {
            Vector3 normalized = vector.normalized;
            float angle = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;
            return angle;
        }

        /// <summary>
        /// Get Flat of vector.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>A new <see cref="Vector3"/> with y-value equals to zero.</returns>
        public static Vector3 ToFlat(this Vector3 source)
        {
            return new Vector3(source.x, 0, source.z);
        }
    }
}
