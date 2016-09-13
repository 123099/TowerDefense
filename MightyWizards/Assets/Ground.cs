using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

    public void OnModelLoaded (GameObject model)
    {
        Transform collider = GameUtils.FindRecursively(model.transform, "Collider");
        if (collider)
        {
            collider.GetComponent<Renderer>().enabled = false;
            collider.gameObject.AddComponent<MeshCollider>();
        }
    }
}
