using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hieki.Utils
{
    public static class SceneDrawer
    {

        /// <summary>
        /// Draw world text in scene/game view
        /// </summary>
        /// <param name="guiSkin"></param>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="fontSize"></param>
        /// <param name="yOffset"></param>
        public static void DrawText(GUISkin guiSkin, string text, Vector3 position, Color? color = null, int fontSize = 0, float yOffset = 0)
        {
#if UNITY_EDITOR
            var prevSkin = GUI.skin;
            if (guiSkin == null)
                Debug.LogWarning("editor warning: guiSkin parameter is null");
            else
                GUI.skin = guiSkin;

            GUIContent textContent = new GUIContent(text);

            GUIStyle style = guiSkin != null ? new GUIStyle(guiSkin.GetStyle("Label")) : new GUIStyle();
            if (color != null)
                style.normal.textColor = (Color)color;
            if (fontSize > 0)
                style.fontSize = fontSize;

            Vector2 textSize = style.CalcSize(textContent);
            Vector3 screenPoint = Camera.current.WorldToScreenPoint(position);

            if (screenPoint.z > 0) // checks necessary to the text is not visible when the camera is pointed in the opposite direction relative to the object
            {
                var worldPosition = Camera.current.ScreenToWorldPoint(new Vector3(screenPoint.x - textSize.x * 0.5f, screenPoint.y + textSize.y * 0.5f + yOffset, screenPoint.z));
                Handles.Label(worldPosition, textContent, style);
            }
            GUI.skin = prevSkin;
#endif
        }

        #region DRAW WIRE CUBES
        public static void DrawWireCubeDebug(Vector3 center, Vector3 size, Quaternion rotation, Color color, float duration)
        {
#if UNITY_EDITOR

            var (down_bottom_left, down_top_left, down_top_right, down_bottom_right, up_bottom_left, up_top_left, up_top_right, up_bottom_right) = GetBoxCorners(center, size, rotation);

            Debug.DrawLine(down_bottom_left, down_top_left, color, duration);
            Debug.DrawLine(down_top_left, down_top_right, color, duration);
            Debug.DrawLine(down_top_right, down_bottom_right, color, duration);
            Debug.DrawLine(down_bottom_right, down_bottom_left, color, duration);

            Debug.DrawLine(up_bottom_left, up_top_left, color, duration);
            Debug.DrawLine(up_top_left, up_top_right, color, duration);
            Debug.DrawLine(up_top_right, up_bottom_right, color, duration);
            Debug.DrawLine(up_bottom_right, up_bottom_left, color, duration);

            Debug.DrawLine(down_bottom_left, up_bottom_left, color, duration);
            Debug.DrawLine(down_top_left, up_top_left, color, duration);
            Debug.DrawLine(down_top_right, up_top_right, color, duration);
            Debug.DrawLine(down_bottom_right, up_bottom_right, color, duration);
#endif
        }

        public static void DrawWireCubeGizmo(Vector3 center, Vector3 size, Quaternion rotation, Color color)
        {
#if UNITY_EDITOR
            var (down_bottom_left, down_top_left, down_top_right, down_bottom_right, up_bottom_left, up_top_left, up_top_right, up_bottom_right) = GetBoxCorners(center, size, rotation);

            Gizmos.color = color;

            Gizmos.DrawLine(down_bottom_left, down_top_left);
            Gizmos.DrawLine(down_top_left, down_top_right);
            Gizmos.DrawLine(down_top_right, down_bottom_right);
            Gizmos.DrawLine(down_bottom_right, down_bottom_left);

            Gizmos.DrawLine(up_bottom_left, up_top_left);
            Gizmos.DrawLine(up_top_left, up_top_right);
            Gizmos.DrawLine(up_top_right, up_bottom_right);
            Gizmos.DrawLine(up_bottom_right, up_bottom_left);

            Gizmos.DrawLine(down_bottom_left, up_bottom_left);
            Gizmos.DrawLine(down_top_left, up_top_left);
            Gizmos.DrawLine(down_top_right, up_top_right);
            Gizmos.DrawLine(down_bottom_right, up_bottom_right);
#endif
        }

        public static void DrawWireCubeHandles(Vector3 center, Vector3 size, Quaternion rotation, Color color, float thickness)
        {
#if UNITY_EDITOR
            var (down_bottom_left, down_top_left, down_top_right, down_bottom_right, up_bottom_left, up_top_left, up_top_right, up_bottom_right) = GetBoxCorners(center, size, rotation);

            Handles.color = color;

            Handles.DrawLine(down_bottom_left, down_top_left, thickness);
            Handles.DrawLine(down_top_left, down_top_right, thickness);
            Handles.DrawLine(down_top_right, down_bottom_right, thickness);
            Handles.DrawLine(down_bottom_right, down_bottom_left, thickness);

            Handles.DrawLine(up_bottom_left, up_top_left, thickness);
            Handles.DrawLine(up_top_left, up_top_right, thickness);
            Handles.DrawLine(up_top_right, up_bottom_right, thickness);
            Handles.DrawLine(up_bottom_right, up_bottom_left, thickness);

            Handles.DrawLine(down_bottom_left, up_bottom_left, thickness);
            Handles.DrawLine(down_top_left, up_top_left, thickness);
            Handles.DrawLine(down_top_right, up_top_right, thickness);
            Handles.DrawLine(down_bottom_right, up_bottom_right, thickness);
#endif
        }


        public static (Vector3 down_bottom_left, Vector3 down_top_left, Vector3 down_top_right, Vector3 down_bottom_right,
            Vector3 up_bottom_left, Vector3 up_top_left, Vector3 up_top_right, Vector3 up_bottom_right
            ) GetBoxCorners(Vector3 center, Vector3 size, Quaternion rotation)
        {
            float halfSizeX = size.x / 2;
            float halfSizeY = size.y / 2;
            float halfSizeZ = size.z / 2;

            Vector3 down_bottom_left = center + rotation * new Vector3(-halfSizeX, -halfSizeY, -halfSizeZ);
            Vector3 down_top_left = center + rotation * new Vector3(-halfSizeX, -halfSizeY, halfSizeZ);
            Vector3 down_top_right = center + rotation * new Vector3(halfSizeX, -halfSizeY, halfSizeZ);
            Vector3 down_bottom_right = center + rotation * new Vector3(halfSizeX, -halfSizeY, -halfSizeZ);

            Vector3 up_bottom_left = center + rotation * new Vector3(-halfSizeX, halfSizeY, -halfSizeZ);
            Vector3 up_top_left = center + rotation * new Vector3(-halfSizeX, halfSizeY, halfSizeZ);
            Vector3 up_top_right = center + rotation * new Vector3(halfSizeX, halfSizeY, halfSizeZ);
            Vector3 up_bottom_right = center + rotation * new Vector3(halfSizeX, halfSizeY, -halfSizeZ);

            return (down_bottom_left, down_top_left, down_top_right, down_bottom_right, up_bottom_left, up_top_left, up_top_right, up_bottom_right);
        }

        #endregion

        #region DRAE WIRE CAPSULES
        public static void DrawWireCapsule(Vector3 p1, Vector3 p2, float radius)
        {
#if UNITY_EDITOR
            // Special case when both points are in the same position
            if (p1 == p2)
            {
                // DrawWireSphere works only in gizmo methods
                Gizmos.DrawWireSphere(p1, radius);
                return;
            }
            using (new Handles.DrawingScope(Handles.color, Handles.matrix))
            {
                Quaternion p1Rotation = Quaternion.LookRotation(p1 - p2);
                Quaternion p2Rotation = Quaternion.LookRotation(p2 - p1);
                // Check if capsule direction is collinear to Vector.up
                float c = Vector3.Dot((p1 - p2).normalized, Vector3.up);
                if (c == 1f || c == -1f)
                {
                    // Fix rotation
                    p2Rotation = Quaternion.Euler(p2Rotation.eulerAngles.x, p2Rotation.eulerAngles.y + 180f, p2Rotation.eulerAngles.z);
                }
                // First side
                Handles.DrawWireArc(p1, p1Rotation * Vector3.left, p1Rotation * Vector3.down, 180f, radius);
                Handles.DrawWireArc(p1, p1Rotation * Vector3.up, p1Rotation * Vector3.left, 180f, radius);
                Handles.DrawWireDisc(p1, (p2 - p1).normalized, radius);
                // Second side
                Handles.DrawWireArc(p2, p2Rotation * Vector3.left, p2Rotation * Vector3.down, 180f, radius);
                Handles.DrawWireArc(p2, p2Rotation * Vector3.up, p2Rotation * Vector3.left, 180f, radius);
                Handles.DrawWireDisc(p2, (p1 - p2).normalized, radius);
                // Lines
                Handles.DrawLine(p1 + p1Rotation * Vector3.down * radius, p2 + p2Rotation * Vector3.down * radius);
                Handles.DrawLine(p1 + p1Rotation * Vector3.left * radius, p2 + p2Rotation * Vector3.right * radius);
                Handles.DrawLine(p1 + p1Rotation * Vector3.up * radius, p2 + p2Rotation * Vector3.up * radius);
                Handles.DrawLine(p1 + p1Rotation * Vector3.right * radius, p2 + p2Rotation * Vector3.left * radius);
            }
#endif
        }

        #endregion

        #region DRAW WIRE CYLINDERS
        public static void DrawWireCylinder(Vector3 p1, Vector3 p2, float radius)
        {
#if UNITY_EDITOR
            using (new Handles.DrawingScope(Handles.color, Handles.matrix))
            {
                Quaternion p1Rotation = Quaternion.LookRotation(p1 - p2);
                Quaternion p2Rotation = Quaternion.LookRotation(p2 - p1);
                // Check if capsule direction is collinear to Vector.up
                float c = Vector3.Dot((p1 - p2).normalized, Vector3.up);
                if (c == 1f || c == -1f)
                {
                    // Fix rotation
                    p2Rotation = Quaternion.Euler(p2Rotation.eulerAngles.x, p2Rotation.eulerAngles.y + 180f, p2Rotation.eulerAngles.z);
                }
                // First side
                Handles.DrawWireDisc(p1, (p2 - p1).normalized, radius);
                // Second side
                Handles.DrawWireDisc(p2, (p1 - p2).normalized, radius);
                // Lines
                Handles.DrawLine(p1 + p1Rotation * Vector3.down * radius, p2 + p2Rotation * Vector3.down * radius);
                Handles.DrawLine(p1 + p1Rotation * Vector3.left * radius, p2 + p2Rotation * Vector3.right * radius);
                Handles.DrawLine(p1 + p1Rotation * Vector3.up * radius, p2 + p2Rotation * Vector3.up * radius);
                Handles.DrawLine(p1 + p1Rotation * Vector3.right * radius, p2 + p2Rotation * Vector3.left * radius);
            }
#endif
        }

        public static void DrawWireCylinder(Vector3 center, float height, float radius)
        {
#if UNITY_EDITOR
            Vector3 p1 = center;
            p1.y -= height / 2;
            Vector3 p2 = center;
            p2.y += height / 2;
            DrawWireCylinder(p1, p2, radius);
#endif
        }

        #endregion

        #region DRAW ARROWS

        public static void ArrowGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
#if UNITY_EDITOR
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
#endif
        }

        public static void ArrowGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
#if UNITY_EDITOR
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
#endif
        }

        public static void ArrowDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
#if UNITY_EDITOR
            Debug.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength);
            Debug.DrawRay(pos + direction, left * arrowHeadLength);
#endif
        }
        public static void ArrowDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
#if UNITY_EDITOR
            Debug.DrawRay(pos, direction, color);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
#endif
        }
        #endregion
    }
}
