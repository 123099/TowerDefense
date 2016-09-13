using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ModelLoader))]
public class ModelLoaderEditor : Editor {

    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUI.color = Color.green;

        if (GUILayout.Button("Refresh", GUILayout.Width(150)))
            ( target as ModelLoader ).LoadModel();

        GUI.color = Color.red;

        if (GUILayout.Button("Unload", GUILayout.Width(150)))
            ( target as ModelLoader ).DeleteModel();

        GUI.color = Color.white;

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
