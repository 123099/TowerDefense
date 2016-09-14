using UnityEngine;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ModelLoader : MonoBehaviour {

    [Tooltip("The path to the model inside the Resources folder, excluding file extension")]
    [SerializeField] [FileChooser("Assets/Resources/", "fbx")] private string model;
    [Tooltip("Set to true to only allow this model to be loaded once for this gameobject")]
    [SerializeField] private bool forceOneInstance = true;
    [Tooltip("The local position to spawn the model at")]
    [SerializeField] private Vector3 position;
    [Tooltip("The local rotation to spawn the model at")]
    [SerializeField] private Vector3 rotation;
    [Tooltip("The local scale to spawn the model with")]
    [SerializeField] private Vector3 scale;
    [Tooltip("The animator controller that controls this model")]
    [SerializeField] private RuntimeAnimatorController animationController;
    [Tooltip("Whether this is a root motion model or not")]
    [SerializeField] private bool applyRootMotion;
    [Tooltip("The material to apply to all the renderers in this model")]
    [SerializeField] private Material material;
    [Tooltip("A list of scripts to attach to this model. These will be set with default values")]
    [SerializeField] [FileChooser("Assets/", "cs")] private string[] scripts;
    
    [Space(10)]
    [SerializeField] private ModelLoadEvent OnModelLoaded;

	private void Awake ()
    {
        LoadModel();
    }

    //Returns the instances of the previous object if it exists. Array is empty if none exist
    private GameObject[] CheckInstanceExists (GameObject resource)
    {
        List<GameObject> instances = new List<GameObject>();
        if (forceOneInstance)
        {
            foreach (Transform t in transform)
                if (t.name == resource.name)
                    instances.Add(t.gameObject);
        }

        return instances.ToArray();
    }

    public void LoadModel ()
    {
        GameObject resource = Resources.Load<GameObject>(model);

        DeleteModel();

        if (resource)
        {
            GameObject modelInstance = Instantiate(resource, transform) as GameObject;
            modelInstance.name = resource.name;
            modelInstance.transform.localPosition = position;
            modelInstance.transform.localRotation = Quaternion.Euler(rotation);
            modelInstance.transform.localScale = scale;

            GameUtils.SetLayer(modelInstance, gameObject.layer, true);

            Animator modelAnimator = modelInstance.GetComponent<Animator>();
            if (modelAnimator)
            {
                modelAnimator.runtimeAnimatorController = animationController;
                modelAnimator.applyRootMotion = applyRootMotion;
            }

            if(material)
            {
                foreach (Renderer renderer in modelInstance.transform.GetComponentsInChildren<Renderer>(true))
                    renderer.material = material;
            }

            foreach (string scriptPath in scripts)
            {
                string[] splitPath = scriptPath.Split('/');
                string scriptName = splitPath[splitPath.Length - 1];
                modelInstance.AddComponent(Types.GetType(scriptName, Assembly.GetAssembly(GetType()).GetName().Name));
            }

            OnModelLoaded.Invoke(modelInstance);
        }
        else
            print("File " + model + " could not be found.");
    }

    public void DeleteModel ()
    {
        GameObject resource = Resources.Load<GameObject>(model);
        GameObject[] previousInstances = CheckInstanceExists(resource);
        foreach (GameObject previousInstance in previousInstances)
            DestroyImmediate(previousInstance);
    }
}
