using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    [Tooltip("The speed with which all the crystals will rotate")]
    [SerializeField] private float speed;
    [Tooltip("The fraction of the distance the object makes every frame")]
    [SerializeField] private float smoothing;
    [Tooltip("The maximum distance the object moves away from it's start position")]
    [SerializeField] private float maxDistance;

	public void OnModelLoaded(GameObject model)
    {
        Transform[] crystals = GameUtils.FindAllRecursively(model.transform, "Crystal");
        foreach (Transform crystal in crystals)
        {
            crystal.gameObject.AddComponent<Rotation>().SetSpeed(speed);
        }

        Transform frame = GameUtils.FindRecursively(model.transform, "Frame");
        Hover hover = frame.gameObject.AddComponent<Hover>();
        hover.SetSmoothing(smoothing);
        hover.SetMaxDistance(maxDistance);
    }
}
