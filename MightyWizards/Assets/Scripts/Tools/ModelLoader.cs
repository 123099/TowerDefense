using UnityEngine;
using System.Collections;
using System.IO;

public class ModelLoader : MonoBehaviour {

    [SerializeField] private string path;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;
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
