using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hieki.Utils
{
    public static class Position
    {
        public static Vector3 Offset(Transform transform, Vector3 offset)
        {
            return transform.position + transform.forward * offset.z + transform.right * offset.x + transform.up * offset.y;
        }

        /// <summary>
        /// Like lerp, but rotund
        /// </summary>
        public static Vector3 Rlerp(Vector3 start, Vector3 end, float centerOffset, float t)
        {
            var center = (start + end) * .5f;
            center.x -= centerOffset;
            var startRelativeCenter = start - center;
            var endRelativeCenter = end - center;

            return Vector3.Slerp(startRelativeCenter, endRelativeCenter, t) + center;
        }

        /// <summary>
        /// Like lerp, but rotund
        /// </summary>
        public static Vector3 Rlerp(Vector3 start, Vector3 end, Vector3 centerOffset, float t)
        {
            var center = (start + end) * .5f;
            center += centerOffset;
            var startRelativeCenter = start - center;
            var endRelativeCenter = end - center;

            return Vector3.Slerp(startRelativeCenter, endRelativeCenter, t) + center;
        }

        public static IEnumerable<Vector3> EvaluateSlerpPoints(Vector3 start, Vector3 end, Vector3 center, int count = 10)
        {
            var startRelativeCenter = start - center;
            var endRelativeCenter = end - center;

            var f = 1f / count;

            for (var i = 0f; i < 1 + f; i += f)
            {
                yield return Vector3.Slerp(startRelativeCenter, endRelativeCenter, i) + center;
            }
        }

        public static bool GetPointOnNavMesh(Vector3 center, float range, out Vector3 result, int times = 10)
        {
            for (int i = 0; i < times; i++)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(center, out hit, range, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            //result = Vector3.zero;
            result = center;
            return false;
        }
    }
}