using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class CreateSOAttribute : PropertyAttribute
{
    public string className;
    public CreateSOAttribute(string SO_className)
    {
        this.className = SO_className;
    }
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(CreateSOAttribute))]
public class CreateSOAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position.width -= 80;
        EditorGUI.ObjectField(position, property, label);

        position.x += position.width;
        position.width = 80;
        if (GUI.Button(position, new GUIContent("Create")))
        {
            var className = attribute as CreateSOAttribute;
            var newScriptableObject = ScripableObjectUtility.CreateAsset(className.className,
                $"Create New {className.className} Asset",
                $"New {className.className} Asset" + Guid.NewGuid().ToString().Substring(0, 8), "");


            if (newScriptableObject)
                property.objectReferenceValue = newScriptableObject;
        }
        EditorGUI.EndProperty();
    }
}
#endif
