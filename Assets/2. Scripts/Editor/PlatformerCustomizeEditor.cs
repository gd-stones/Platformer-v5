using UnityEditor;
using System.Collections.Generic;

namespace StonesGaming
{
    [CustomEditor(typeof(PlatformerCustomize))]
    public class PlatformerCustomizeEditor : Editor
    {
        private bool _coreFoldout;
        private bool _attackFoldout;
        private bool _pushFoldout;
        private bool _deadFoldout;
        private bool _jumpFoldout;
        private bool _healthFoldout;

        private Dictionary<string, SerializedProperty> _properties = new Dictionary<string, SerializedProperty>();

        private void OnEnable()
        {
            _properties.Clear();
            SerializedProperty property = serializedObject.GetIterator();

            while (property.NextVisible(true))
            {
                _properties[property.name] = property.Copy();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Core Header
            _coreFoldout = EditorGUILayout.Foldout(_coreFoldout, "Core");
            if (_coreFoldout)
            {
                EditorGUILayout.PropertyField(_properties["_engine"]);
                //EditorGUILayout.PropertyField(_properties["_audioSource"]);
            }

            // Attack Header
            _attackFoldout = EditorGUILayout.Foldout(_attackFoldout, "Attack");
            if (_attackFoldout)
            {
                EditorGUILayout.PropertyField(_properties["turnAttack"]);
                EditorGUILayout.PropertyField(_properties["_fire1"]);
                EditorGUILayout.PropertyField(_properties["_fire2"]);
                EditorGUILayout.PropertyField(_properties["_fire3"]);
                EditorGUILayout.PropertyField(_properties["_fireRun1"]);
                EditorGUILayout.PropertyField(_properties["_fireRun2"]);
                EditorGUILayout.PropertyField(_properties["_fireRun3"]);
                EditorGUILayout.PropertyField(_properties["_firePointRight"]);
                EditorGUILayout.PropertyField(_properties["_firePointLeft"]);
                //EditorGUILayout.PropertyField(_properties["_attackClip"]);
            }

            // Push Header
            _pushFoldout = EditorGUILayout.Foldout(_pushFoldout, "Push");
            if (_pushFoldout)
            {
                EditorGUILayout.PropertyField(_properties["_pushSpeed"]);
                //EditorGUILayout.PropertyField(_properties["_groundSpeed"]);
            }

            // Dead Header
            _deadFoldout = EditorGUILayout.Foldout(_deadFoldout, "Dead");
            if (_deadFoldout)
            {
                EditorGUILayout.PropertyField(_properties["_playerHitPrefab"]);
                //EditorGUILayout.PropertyField(_properties["_hitClip"]);
            }

            // Jump Header
            _jumpFoldout = EditorGUILayout.Foldout(_jumpFoldout, "Jump");
            if (_jumpFoldout)
            {
                EditorGUILayout.PropertyField(_properties["_jumpVfx1"]);
                EditorGUILayout.PropertyField(_properties["_jumpVfx2"]);
                //EditorGUILayout.PropertyField(_properties["_jumpClip"]);
                EditorGUILayout.PropertyField(_properties["isSkipJumpSe"]);
            }

            // Health Header
            _healthFoldout = EditorGUILayout.Foldout(_healthFoldout, "Health");
            if (_healthFoldout)
            {
                EditorGUILayout.PropertyField(_properties["_originalHealth"]);
                //EditorGUILayout.PropertyField(_properties["health"]);
                EditorGUILayout.PropertyField(_properties["_damagePlayer"]);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
