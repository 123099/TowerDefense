using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;

[CustomPropertyDrawer(typeof(FileChooser))]
public class FileChooserPropertyDrawer : PropertyDrawer {

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        string currentValue = property.stringValue;
        FileChooser fileChooser = attribute as FileChooser;
        
        int currentIndex = Array.FindIndex(fileChooser.files, x => x.Contains(currentValue));

        if (currentIndex == -1)
        {
            currentIndex = 0;
            Debug.Log("Couldn't find property string " + currentValue + " in the options");
        }

        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int index = EditorGUI.Popup(position, currentIndex, fileChooser.files);
        property.stringValue = fileChooser.files[index];

        EditorGUI.EndProperty();
    }
}


