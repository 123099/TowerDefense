using UnityEngine;
using System.Collections;
using UnityEditor;

public class ModelLoaderMenu : Editor {

	[MenuItem("Model Loader/Refresh All")]
    public static void RefreshAll ()
    {
        ModelLoader[] modelLoaders = GameObject.FindObjectsOfType<ModelLoader>();
        foreach (ModelLoader loader in modelLoaders)
            loader.LoadModel();
    }

    [MenuItem("Model Loader/Unload All")]
    public static void UnloadAll ()
    {
        ModelLoader[] modelLoaders = GameObject.FindObjectsOfType<ModelLoader>();
        foreach (ModelLoader loader in modelLoaders)
            loader.DeleteModel();
    }
}
