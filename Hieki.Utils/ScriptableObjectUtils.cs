using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hieki.Utils
{
    public class ScripableObjectUtils
    {
        /// <summary>
        /// Open Save File pannel to select a file location before creating a ScriptableObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="defaultName"></param>
        /// <param name="extension">should be :"asset"</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static T CreateAsset<T>(string title, string defaultName, string message = "") where T : ScriptableObject
        {
#if UNITY_EDITOR
            if (string.IsNullOrWhiteSpace(defaultName))
            {
                defaultName = "NewAsset";
            }

            string filePath = EditorUtility.SaveFilePanelInProject(title, defaultName, "asset", message);

            if (!string.IsNullOrEmpty(filePath))
            {
                //UnityEngine.Debug.Log(filePath);
                T data = ScriptableObject.CreateInstance<T>();

                return (T)Create(data, filePath);
            }

            else
#endif
                return null;
        }
        public static ScriptableObject CreateAsset(string className, string title, string defaultName, string message = "")
        {
#if UNITY_EDITOR
            if (string.IsNullOrWhiteSpace(defaultName))
            {
                defaultName = "NewAsset";
            }
            string filePath = EditorUtility.SaveFilePanelInProject(title, defaultName, "asset", message);

            if (!string.IsNullOrEmpty(filePath))
            {
                //UnityEngine.Debug.Log(filePath);
                var data = ScriptableObject.CreateInstance(className);

                return Create(data, filePath);
            }

            else
#endif
                return null;
        }

        static ScriptableObject Create(ScriptableObject asset, string path)
        {
#if UNITY_EDITOR
            if (!asset)
            {
                Debug.LogError("failed to create asset");
                return null;
            }
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            //EditorUtility.RevealInFinder(Path.GetDirectoryName(AssetDatabase.GetAssetPath(asset)));

            Selection.activeObject = asset;
#endif
            return asset;
        }
    }
}


