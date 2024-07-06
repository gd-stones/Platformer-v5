using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PolicyDataSO))]
public class PolicyCustomEditor : ExtendedCustomEditor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();

        GUILayout.Space(10);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        DrawHeader("Privacy Policy Link (URL)");
        DrawTextArea("_privacyPolicyLink");

        EditorGUILayout.EndVertical();

        GUILayout.Space(10);
        serializedObject.ApplyModifiedProperties();
        DrawWatermark();
    }
}
