using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities
{
    public static class ScreenSpace
    {
        #region Pointer
        /// <summary>
        /// is pointer over UI Game Object, which allow raycast target
        /// </summary>
        /// <returns></returns>
        public static bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

        /// <summary>
        /// is pointer over ui which has <see cref="T"/> Component
        /// </summary>
        /// <returns></returns>
        public static bool IsPointerOverIgnoredUI<T>() where T : Component
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, result);

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].gameObject.GetComponent<T>())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}