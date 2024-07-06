using UnityEditor;
using UnityEngine;

public class ExtendedCustomEditor : Editor
{
    protected void DrawHeader(string label)
    {
        //EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUIStyle labelStyle = new GUIStyle(WhiteBackground());
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.fontSize = 12;
        labelStyle.normal.textColor = Color.white;
        labelStyle.fixedHeight = 26;
 
        EditorGUILayout.LabelField(label, labelStyle, GUILayout.ExpandWidth(true));
        GUILayout.Space(8);
        //EditorGUILayout.EndVertical();
        GUILayout.Space(3);
    }

    protected void DrawLabel(string label)
    {
        Color color = new Color(1f, 1f, 1f, .1f);
        GUIStyle labelStyle = new GUIStyle(ColorBackground(color));
        labelStyle.alignment = TextAnchor.MiddleCenter;
        //labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.fontSize = 11;
        labelStyle.normal.textColor = new Color(1f, 1f, 1f, .9f);
        EditorGUILayout.LabelField(label, labelStyle, GUILayout.ExpandWidth(true));
        GUILayout.Space(3);
    }

    protected void DrawProperty(string propertyPath)
    {
        SerializedProperty prop = serializedObject.FindProperty(propertyPath);
        EditorGUILayout.PropertyField(prop);
        GUILayout.Space(2);
    }

    protected void DrawCenterLabel(string label)
    {
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        style.fontSize = 11;
        EditorGUILayout.LabelField(label, style, GUILayout.ExpandWidth(true));
    }

    protected void DrawProperty(string propertyPath, GUIContent option)
    {
        SerializedProperty prop = serializedObject.FindProperty(propertyPath);
        EditorGUILayout.PropertyField(prop, option);
        GUILayout.Space(2);
    }

    protected void DrawTextArea(string propertyPath, int lineHeight = 2)
    {
        GUIStyle style = new GUIStyle(EditorStyles.textArea);
        style.wordWrap = true;
        style.fixedHeight = style.lineHeight * lineHeight;

        var val = serializedObject.FindProperty(propertyPath);
        val.stringValue = EditorGUILayout.TextArea(val.stringValue, style);
    }

    protected void DrawWatermark()
    {
        GUIStyle wm = new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.BoldAndItalic, fontSize = 10 };
        wm.normal.textColor = Color.gray;
        GUILayout.Label("Created by Sieu Pham.", wm);
    }

    protected GUIStyle WhiteBackground()
    {
        GUIStyle bg = new GUIStyle();
        //bg.normal.background = MakeTex(1, 1, new Color(0f, 0f, 0f, .4f)); // black
        //bg.normal.background = MakeTex(1, 1, new Color(1f, 0f, 0f, .4f)); // red
        //bg.normal.background = MakeTex(1, 1, new Color(0f, 1f, 0f, .4f)); // green
        //bg.normal.background = MakeTex(1, 1, new Color(0f, 0f, 1f, .4f)); // blue
        //bg.normal.background = MakeTex(1, 1, new Color(1f, 1f, 0f, .4f)); // yellow
        //bg.normal.background = MakeTex(1, 1, new Color(1f, 0f, 1f, .4f)); // purple
        bg.normal.background = MakeTex(1, 1, new Color(0f, 1f, 1f, .4f)); // cyan
        //bg.normal.background = MakeTex(1, 1, new Color(1f, 1f, 1f, .4f)); // white
        return bg;
    }

    protected GUIStyle ColorBackground(Color color)
    {
        GUIStyle bg = new GUIStyle();
        bg.normal.background = MakeTex(1, 1, color);
        return bg;
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }
}
