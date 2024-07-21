using UnityEngine;

namespace Hieki.Utils
{
    public static class Rotation
    {
        public static Vector3 LerpAngle(Vector3 from, Vector3 to, float t)
        {
            Vector3 angle = new Vector3(
                angle.x = Mathf.LerpAngle(from.x, to.x, t),
                angle.y = Mathf.LerpAngle(from.y, to.y, t),
                angle.z = Mathf.LerpAngle(from.z, to.z, t)
                );
            return angle;
        }

        public static Vector3 RotateVector(Vector3 dir, Vector3 euler)
        {
            return Quaternion.Euler(euler) * dir;
        }

        public static Vector3 RotateVector(Vector3 dir, float eulerAnglesY)
        {
            return Quaternion.Euler(0, eulerAnglesY, 0) * dir;
        }

        public static Vector3 RotatePosition(Vector3 pos, float eulerAnglesY, Vector3 origin)
        {
            return Quaternion.Euler(0, eulerAnglesY, 0) * (pos - origin);
        }
    }
}
