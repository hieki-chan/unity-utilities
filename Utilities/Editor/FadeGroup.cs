//Fade doc: https://docs.unity3d.com/ScriptReference/EditorGUILayout.BeginFadeGroup.html
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace Utilities.Editor
{
    /// <summary>
    /// hiding and showing a group of GUI elements. **only work in editor
    /// </summary>
    public class FadeGroup
    {
        public bool visible { get; private set; } = true;
        UnityEditor.Editor editor;
        GUIStyle areaStyle;
        GUIStyle labelStyle;
        AnimBool showExtra;
        bool alwaysVisible;
        bool indentLevel;
        bool fadeCalled = false;

        //Styles
        /// <summary>
        /// Text Area style (dark)
        /// </summary>
        public static GUIStyle DefaultAreaStyle_01 { get; private set; } = new GUIStyle(GUI.skin.textField)
        {
            margin = new RectOffset(0, 0, 0, 0),
            padding = new RectOffset(0, 0, 0, 0),
        };
        /// <summary>
        /// button style (grey)
        /// </summary>
        public static GUIStyle DefaultAreaStyle_02 { get; private set; } = new GUIStyle(GUI.skin.button)
        {
            padding = new RectOffset(0, 0, 0, 0),
            border = new RectOffset(0, 0, 0, 0),
        };
        /// <summary>
        /// box style (dark)
        /// </summary>
        public static GUIStyle DefaultLabelStyle_01 { get; private set; } = new GUIStyle(GUI.skin.box)
        {
            stretchHeight = true,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold,
        };
        /// <summary>
        /// label style (grey)
        /// </summary>
        public static GUIStyle DefaultLabelStyle_02 { get; private set; } = new GUIStyle(GUI.skin.box)
        {
            padding = new RectOffset(),
            margin = new RectOffset(),
            stretchWidth = true,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold,
            normal = new GUIStyleState()
            {
                background = EditorUtils.MakeBackgroundTexture(10, 10, Color.grey),
            }
        };

        /// <summary>
        /// Create a new FadeGroup. Fade group should be static field to save the visible state.
        /// (Fade group use for custom property drawer sometimes not work  well)
        /// *****How to use:
        /// First, call <see cref="Begin(string)"/> to begin your area.
        /// Then, check <see cref="Fade"/> if you want to draw fields when the area is already open.
        /// Finally, use <see cref="End"/> to end your area.
        /// </summary>
        /// <param name="editor">the editor which you want to edit. When you set it to null, Repaint may not work well</param>
        /// <param name="alwaysVisible">is FadeGroup always visible?</param>
        /// <param name="areaStyle">your area style, if this style is null, It's will be set to default style!</param>
        /// <param name="labelStyle">your label style, if this style is null, It's will be set to default style!</param>
        public FadeGroup(UnityEditor.Editor editor = null, bool alwaysVisible = false, GUIStyle areaStyle = null, GUIStyle labelStyle = null)
        {         
            this.editor = editor;
            this.areaStyle = areaStyle;
            this.labelStyle = labelStyle;
            this.alwaysVisible = alwaysVisible;

            if (alwaysVisible)
                showExtra = new AnimBool(true);
            else
                showExtra = new AnimBool(false);

            //create new styles if styles isn't given
            if (this.areaStyle == null)
                this.areaStyle = DefaultAreaStyle_01;

            if (this.labelStyle == null)
                this.labelStyle = DefaultLabelStyle_01;
            if(editor)
                showExtra.valueChanged.AddListener(editor.Repaint);
        }
        /// <summary>
        ///Begins a group that can be be hidden/shown and the transition will be animated.
        ///And remember to call <see cref="End"/>
        /// </summary>
        public void Begin(string header = "")
        {
            if(areaStyle != null)
                EditorGUILayout.BeginVertical(areaStyle);
            else
                EditorGUILayout.BeginVertical();

            if(GUILayout.Button(header, labelStyle) && !alwaysVisible)
            {
                showExtra.target = !showExtra.target;
                if(editor)
                    editor.Repaint();
            }
        }

        public void Begin(string closedHeader, string openedHeader)
        {
            if (!visible)
                Begin(closedHeader);
            else
                Begin(openedHeader);
        }

        /// <summary>
        /// Hide/Show animations
        /// </summary>
        /// <returns>If the group is visible or not</returns>
        public bool Fade(bool indentLevel = true)
        {
            this.indentLevel = indentLevel;
            fadeCalled = true;
            if(Event.current.type == EventType.Repaint)
            {
                //Code when repaint
            }
            //we don't need to call BeginFadeGroup when this group is always visible
            if (!alwaysVisible)
                visible = EditorGUILayout.BeginFadeGroup(showExtra.faded);
            EditorGUILayout.BeginVertical();
            if (indentLevel)
                EditorGUI.indentLevel++;
            return visible;
        }

        /// <summary>
        /// Hide/Show animations, and background color!
        /// </summary>
        /// <param name="indentLevel"></param>
        /// <param name="backgroundColor">background color of fields</param>
        /// <returns></returns>
        public bool Fade(bool indentLevel, Color backgroundColor)
        {
            GUI.backgroundColor = backgroundColor;
            return Fade(indentLevel);
        }

        /// <summary>
        /// close a group started with <see cref="Begin"/>
        /// </summary>
        /// <param name="space">Space with the next field</param>
        public void End(int space = 5)
        {
            if (indentLevel)
                EditorGUI.indentLevel--;
            if(fadeCalled)
                EditorGUILayout.EndVertical();
            //GUILayout.Space(4);
            // we don't need to call EndFadeGroup End if always visible
            if (!alwaysVisible && fadeCalled)
                EditorGUILayout.EndFadeGroup();
            fadeCalled = false;
            EditorGUILayout.EndVertical();
            GUILayout.Space(space);
        }

        public static bool operator !(FadeGroup a)
        {
            return a == null;
        }

        ///<example> EXAMPLE
        /// EDITOR GROUP
        ///
        /// static FadeGroup exampleGroup;
        /// void DrawGroup()
        /// {
        ///     if (!exampleGroup)
        ///         exampleGroup = new FadeGroup(this);
        ///     exampleGroup.Begin("Example");
        ///     if (exampleGroup.Fade())
        ///     {
        ///         DrawComponent(displayQuality, "Display quality");
        ///         DrawProperty(sensitivity, "Sensitivity");
        ///         //....
        ///     }    
        ///     exampleGroup.End();
        /// }
        ///
        /// PROPERTY DRAWER GROUP
        /// static FadeGroup drawerGroup;
        /// void OnGUI(/*....*/)
        /// {
        ///     if (!drawerGroup)
        ///         drawerGroup = new FadeGroup();
        ///     EditorGUI.BeginProperty(position, label, property);    
        ///     drawerGroup.Begin("Example");
        ///     if (drawerGroup.Fade(false, Color.red))
        ///     {
        ///         //your custom property drawer
        ///         //code here
        ///     }    
        ///     drawerGroup.End();
        ///     EditorGUI.EndProperty();
        /// }
        /// 
        /// NESTED FADEGROUP
        /// fg1.Begin();
        /// if(fg1.Fade())
        /// {
        ///     fg2.Begin();
        ///     // you code here....
        ///     // make sure end with 0 space
        ///     fg2.End(0);
        /// }
        /// fg1.End();
        ///</example>
    }
}