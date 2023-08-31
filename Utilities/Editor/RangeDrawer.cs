using UnityEditor;
using UnityEngine;

namespace Utilities.Editor
{
    [CustomPropertyDrawer(typeof(Range))]
    internal class RangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            //Label
            var contentPos = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            float pos = contentPos.x;
            //Get properties
            var min = property.FindPropertyRelative("min");
            var max = property.FindPropertyRelative("max");
            var value = property.FindPropertyRelative("value");

            int level = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            contentPos.width *= .5f;
            contentPos.width -= 20;

            EditorGUI.PropertyField(contentPos, min, GUIContent.none);
            contentPos.x += contentPos.width + 10;
            EditorGUI.LabelField(contentPos, new GUIContent("To"));
            contentPos.x += 30;
            EditorGUI.PropertyField(contentPos, max, GUIContent.none);
            //Draw value as a slidier
            contentPos.y += contentPos.height;
            contentPos.x = pos;
            contentPos.width +=20;
            contentPos.width *= 2;
            EditorGUI.Slider(contentPos, value, min.floatValue, max.floatValue, GUIContent.none);
            EditorGUILayout.Space(16);

            if (min.floatValue < max.floatValue && (value.floatValue > max.floatValue || value.floatValue < min.floatValue))
                value.floatValue = (min.floatValue);
            if (min.floatValue > max.floatValue && (value.floatValue < max.floatValue || value.floatValue > min.floatValue))
                value.floatValue = (max.floatValue);
            EditorGUI.indentLevel = level;
            EditorGUI.EndProperty();
        }
    }
}