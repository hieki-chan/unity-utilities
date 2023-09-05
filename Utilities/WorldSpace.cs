using UnityEngine;

namespace Utilities
{
    public static class WorldSpace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offset"></param>
        /// <returns>new position</returns>
        public static Vector3 GetPositionByOffset(Transform transform, Vector3 offset)
           => transform.position + transform.right * offset.x + transform.forward * offset.z + transform.up * offset.y;
        /// <summary>
        /// Get terrain's difference height between min and max height
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="maxDistance"></param>
        /// <param name="checkLayer"></param>
        /// <returns></returns>
        public static float GetHeightDifference(Vector3 origin, int maxDistance, LayerMask checkLayer)
        {
            float max = 0F;
            float min = 0F;
            Vector3[] testPoints = new Vector3[0];
            RaycastHit hitInfo;
            foreach (Vector3 point in testPoints)
            {
                //Debug.DrawRay(point.position, new Vector3(0, -10, 0), Color.red);
                if (Physics.Raycast(point, Vector3.down, out hitInfo, maxDistance, checkLayer))
                {
                    if (hitInfo.point.y > max)
                    {
                        max = hitInfo.point.y;
                    }
                    else if (hitInfo.point.y < min)
                    {
                        min = hitInfo.point.y;
                    }
                }
            }
            return (max - min);
        }
    }
}