using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(Wave.Unit))]
public class WaveUnitPropertyDrawer : PropertyDrawer {

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty model = property.FindPropertyRelative("modelPrefab");
        SerializedProperty spawnOffset = property.FindPropertyRelative("spawnOffset");
        SerializedProperty spawnDelay = property.FindPropertyRelative("spawnDelayAfterLastUnit");

        if(model.objectReferenceValue != null)
            label.text = model.objectReferenceValue.name + " (" + spawnDelay.floatValue + ")";

        EditorGUI.BeginProperty(position, label, property);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        EditorGUIUtility.labelWidth = 90;
        position = EditorGUI.PrefixLabel(position, label);

        float x = position.x;
        float w = position.width;

        Rect modelRect = new Rect(x, position.y, 0.2f * w, position.height);
        Rect spawnOffsetRect = new Rect(x + 0.25f * w, position.y, 0.5f * w, position.height);
        Rect spawnDelayRect = new Rect(x + 0.8f * w, position.y, 0.2f * w, position.height);

        EditorGUI.PropertyField(modelRect, model, GUIContent.none);
        EditorGUI.PropertyField(spawnOffsetRect, spawnOffset, GUIContent.none);
        EditorGUI.PropertyField(spawnDelayRect, spawnDelay, GUIContent.none);

        EditorGUIUtility.labelWidth = 0;
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

}
