using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities
{
    public static class RuntimeUtils
    {
        public static Vector3 GetPositionByOffset(Transform transform, Vector3 offset)
            => transform.position + transform.right * offset.x + transform.forward * offset.z + transform.up * offset.y;
 

        #region Pointer
        /// <summary>
        /// is pointer over UI
        /// </summary>
        /// <returns></returns>
        public static bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

        /// <summary>
        /// is pointer over ui which has component: Utilities.UI.IgnorePointerOver
        /// </summary>
        /// <returns></returns>
        public static bool IsPointerOverIgnoredUI()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, result);

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].gameObject.GetComponent<UI.IgnorePointerOver>())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}