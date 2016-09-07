using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Pan : MonoBehaviour {

    [Tooltip("The factor by which to blend between the start and target positions each frame")]
    [SerializeField] private float smoothing;

    private Transform startPosition;
    private Transform targetPosition;

	public void PanTowards(Transform targetPosition)
    {
        startPosition = transform;
        this.targetPosition = targetPosition;
    }

    private void Update ()
    {
        if (targetPosition)
        {
            transform.position = Vector3.Slerp(startPosition.position, targetPosition.position, smoothing);
        }
    }
}
