using UnityEngine;
using System.Collections;
using System.IO;

public class ModelLoader : MonoBehaviour {

    [Tooltip("The path to the model inside the Resources folder, excluding file extension")]
    [SerializeField] private string path;
    [Tooltip("The local position to spawn the model at")]
    [SerializeField] private Vector3 position;
    [Tooltip("The local rotation to spawn the model at")]
    [SerializeField] private Vector3 rotation;
    [Tooltip("The local scale to spawn the model with")]
    [SerializeField] private Vector3 scale;

	private void Awake ()
    {
        GameObject resource = Resources.Load<GameObject>(path);
        if(resource)
        { 
            GameObject model = Instantiate(resource, transform) as GameObject;
            model.transform.localPosition = position;
            model.transform.localRotation = Quaternion.Euler(rotation);
            model.transform.localScale = scale;
        }
        else
            print("File " + path + " could not be found.");
    }
}
