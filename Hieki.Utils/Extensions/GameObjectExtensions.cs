using System;
using System.Linq;
using UnityEngine;

namespace Hieki.Utils
{
    public static class GameObjectExtensions
    {
        public static T GetComponentInChildrenExcludeParent<T>(this GameObject obj, bool includeInactive = false) where T : Component
        {
            var components = obj.GetComponentsInChildren<T>(includeInactive);

            return components.FirstOrDefault(childComponent =>
                childComponent.transform != obj.transform);
        }

        public static void ForeachChilds(this GameObject gameObject, Action<GameObject> callback)
        {
            int childCount = gameObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                callback?.Invoke(child.gameObject);
            }
        }

        public static void SetLayerRecursively(this GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }

        public static void SetLayerRecursively(this GameObject obj, int fromLayer, int toLayer)
        {
            if(obj.layer == fromLayer)
                obj.layer = toLayer;
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetLayerRecursively(fromLayer, toLayer);
            }
        }
    }
}
