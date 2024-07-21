using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class HelpAttribute : PropertyAttribute
{
    public readonly string text;

    // MessageType exists in UnityEditor namespace and can throw an exception when used outside the editor.
    // We spoof MessageType at the bottom of this script to ensure that errors are not thrown when
    // MessageType is unavailable.
    public readonly MessageType type;

    /// <summary>
    /// Adds a HelpBox to the Unity property inspector above this field.
    /// </summary>
    /// <param name="text">The help text to be displayed in the HelpBox.</param>
    /// <param name="type">The icon to be displayed in the HelpBox.</param>
    public HelpAttribute(MessageType type = MessageType.Info)
    {
        this.text = "";
        this.type = type;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HelpAttribute))]
public class HelpDrawer : PropertyDrawer
{
    // Used for top and bottom padding between the text and the HelpBox border.
    const int paddingHeight = 8;

    // Used to add some margin between the the HelpBox and the property.
    const int marginHeight = 2;

    //  Global field to store the original (base) property height.
    float baseHeight = 0;

    // Custom added height for drawing text area which has the MultilineAttribute.
    float addedHeight = 0;

    /// <summary>
    /// A wrapper which returns the PropertyDrawer.attribute field as a HelpAttribute.
    /// </summary>
    HelpAttribute helpAttribute { get { return (HelpAttribute)attribute; } }

    /// <summary>
    /// A helper property to check for MultiLineAttribute.
    /// </summary>
    MultilineAttribute multilineAttribute
    {
        get
        {
            var attributes = fieldInfo.GetCustomAttributes(typeof(MultilineAttribute), true);
            return attributes != null && attributes.Length > 0 ? (MultilineAttribute)attributes[0] : null;
        }
    }


    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
    {
        // We store the original property height for later use...
        baseHeight = base.GetPropertyHeight(prop, label);

        // This stops icon shrinking if text content doesn't fill out the container enough.
        float minHeight = paddingHeight * 5;

        // Calculate the height of the HelpBox using the GUIStyle on the current skin and the inspector
        // window's currentViewWidth.
        var content = new GUIContent(GetText(prop));
        var style = GUI.skin.GetStyle("helpbox");

        var height = style.CalcHeight(content, EditorGUIUtility.currentViewWidth);

        // We add tiny padding here to make sure the text is not overflowing the HelpBox from the top
        // and bottom.
        height += marginHeight * 2;

        // Since we draw a custom text area with the label above if our property contains the
        // MultilineAttribute, we need to add some extra height to compensate. This is stored in a
        // seperate global field so we can use it again later.
        if (multilineAttribute != null && prop.propertyType == SerializedPropertyType.String)
        {
            addedHeight = 36f;
        }

        // If the calculated HelpBox is less than our minimum height we use this to calculate the returned
        // height instead.
        return height > minHeight ? height + baseHeight + addedHeight : minHeight + baseHeight + addedHeight;
    }


    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        // We get a local reference to the MultilineAttribute as we use it twice in this method and it
        // saves calling the logic twice for minimal optimization, etc...
        var multiline = multilineAttribute;

        EditorGUI.BeginProperty(position, label, prop);

        // Copy the position out so we can calculate the position of our HelpBox without affecting the
        // original position.
        var helpPos = position;

        helpPos.height -= baseHeight + marginHeight;


        if (multiline != null)
        {
            helpPos.height -= addedHeight;
        }

        // Renders the HelpBox in the Unity inspector UI.
        EditorGUI.HelpBox(helpPos, GetText(prop), helpAttribute.type);
        position.y += helpPos.height + marginHeight;
        position.height = baseHeight;

        EditorGUI.EndProperty();
    }

    string GetText(SerializedProperty property)
    {
        string text = helpAttribute.text;

        try
        {
            text = property.stringValue;
        }

        catch
        {
            Debug.LogError("HelpAttribute only support string type!");
        }

        return text;
    }
}
#else
// Replicate MessageType Enum if we are not in editor as this enum exists in UnityEditor namespace.
public enum MessageType
{
    None,
    Info,
    Warning,
    Error,
}
#endif